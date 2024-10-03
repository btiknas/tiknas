using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.Http.Modeling;
using Tiknas.MultiTenancy;
using Tiknas.Reflection;
using Tiknas.Threading;
using Tiknas.Tracing;

namespace Tiknas.Http.Client.DynamicProxying;

public class ApiDescriptionFinder : IApiDescriptionFinder, ITransientDependency
{
    public ICancellationTokenProvider CancellationTokenProvider { get; set; }
    protected IApiDescriptionCache Cache { get; }
    protected TiknasCorrelationIdOptions TiknasCorrelationIdOptions { get; }
    protected ICorrelationIdProvider CorrelationIdProvider { get; }
    protected ICurrentTenant CurrentTenant { get; }

    public ApiDescriptionFinder(
        IApiDescriptionCache cache,
        IOptions<TiknasCorrelationIdOptions> tiknasCorrelationIdOptions,
        ICorrelationIdProvider correlationIdProvider,
        ICurrentTenant currentTenant)
    {
        Cache = cache;
        TiknasCorrelationIdOptions = tiknasCorrelationIdOptions.Value;
        CorrelationIdProvider = correlationIdProvider;
        CurrentTenant = currentTenant;
        CancellationTokenProvider = NullCancellationTokenProvider.Instance;
    }

    public async Task<ActionApiDescriptionModel> FindActionAsync(
        HttpClient client,
        string baseUrl,
        Type serviceType,
        MethodInfo method)
    {
        var apiDescription = await GetApiDescriptionAsync(client, baseUrl);

        //TODO: Cache finding?

        var methodParameters = method.GetParameters().ToArray();

        foreach (var module in apiDescription.Modules.Values)
        {
            foreach (var controller in module.Controllers.Values)
            {
                if (!controller.Implements(serviceType))
                {
                    continue;
                }

                foreach (var action in controller.Actions.Values)
                {
                    if (action.Name == method.Name && action.ParametersOnMethod.Count == methodParameters.Length)
                    {
                        var found = true;

                        for (int i = 0; i < methodParameters.Length; i++)
                        {
                            if (!TypeMatches(action.ParametersOnMethod[i], methodParameters[i]))
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            return action;
                        }
                    }
                }
            }
        }

        throw new TiknasException($"Could not find remote action for method: {method} on the URL: {baseUrl}");
    }

    public virtual async Task<ApplicationApiDescriptionModel> GetApiDescriptionAsync(HttpClient client, string baseUrl)
    {
        return await Cache.GetAsync(baseUrl, () => GetApiDescriptionFromServerAsync(client, baseUrl));
    }

    public static JsonSerializerOptions DeserializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected virtual async Task<ApplicationApiDescriptionModel> GetApiDescriptionFromServerAsync(
        HttpClient client,
        string baseUrl)
    {
        var requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            baseUrl.EnsureEndsWith('/') + "api/tiknas/api-definition"
        );

        AddHeaders(requestMessage);

        var response = await client.SendAsync(
            requestMessage,
            CancellationTokenProvider.Token
        );

        if (!response.IsSuccessStatusCode)
        {
            throw new TiknasException("Remote service returns error! StatusCode = " + response.StatusCode);
        }

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApplicationApiDescriptionModel>(content, DeserializeOptions)!;

        return result;
    }

    protected virtual void AddHeaders(HttpRequestMessage requestMessage)
    {
        //CorrelationId
        requestMessage.Headers.Add(TiknasCorrelationIdOptions.HttpHeaderName, CorrelationIdProvider.Get());

        //TenantId
        if (CurrentTenant.Id.HasValue)
        {
            //TODO: Use TiknasAspNetCoreMultiTenancyOptions to get the key
            requestMessage.Headers.Add(TenantResolverConsts.DefaultTenantKey, CurrentTenant.Id.Value.ToString());
        }

        //Culture
        //TODO: Is that the way we want? Couldn't send the culture (not ui culture)
        var currentCulture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
        if (!currentCulture.IsNullOrEmpty())
        {
            requestMessage.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(currentCulture));
        }

        //X-Requested-With
        requestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");
    }

    protected virtual bool TypeMatches(MethodParameterApiDescriptionModel actionParameter, ParameterInfo methodParameter)
    {
        return actionParameter.Type.ToUpper() == TypeHelper.GetFullNameHandlingNullableAndGenerics(methodParameter.ParameterType).ToUpper();
    }
}
