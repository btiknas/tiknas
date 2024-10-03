using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client;
using Tiknas.Http.Client.Authentication;
using Tiknas.Json.SystemTextJson;
using Tiknas.MultiTenancy;
using Tiknas.Tracing;

namespace Tiknas.Dapr;

public class TiknasDaprClientFactory : ITiknasDaprClientFactory, ISingletonDependency
{
    protected TiknasDaprOptions DaprOptions { get; }
    protected JsonSerializerOptions JsonSerializerOptions { get; }
    protected IDaprApiTokenProvider DaprApiTokenProvider { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected ICorrelationIdProvider CorrelationIdProvider { get; }
    protected IOptions<TiknasCorrelationIdOptions> TiknasCorrelationIdOptions { get; }
    protected IRemoteServiceHttpClientAuthenticator RemoteServiceHttpClientAuthenticator { get; }

    public TiknasDaprClientFactory(
        IOptions<TiknasDaprOptions> options,
        IOptions<TiknasSystemTextJsonSerializerOptions> systemTextJsonSerializerOptions,
        IDaprApiTokenProvider daprApiTokenProvider,
        ICurrentTenant currentTenant, ICorrelationIdProvider correlationIdProvider,
        IOptions<TiknasCorrelationIdOptions> tiknasCorrelationIdOptions,
        IRemoteServiceHttpClientAuthenticator remoteServiceHttpClientAuthenticator)
    {
        DaprApiTokenProvider = daprApiTokenProvider;
        CurrentTenant = currentTenant;
        CorrelationIdProvider = correlationIdProvider;
        TiknasCorrelationIdOptions = tiknasCorrelationIdOptions;
        RemoteServiceHttpClientAuthenticator = remoteServiceHttpClientAuthenticator;
        DaprOptions = options.Value;
        JsonSerializerOptions = CreateJsonSerializerOptions(systemTextJsonSerializerOptions.Value);
    }

    public virtual Task<DaprClient> CreateAsync(Action<DaprClientBuilder>? builderAction = null)
    {
        var builder = new DaprClientBuilder()
            .UseJsonSerializationOptions(JsonSerializerOptions);

        if (!DaprOptions.HttpEndpoint.IsNullOrWhiteSpace())
        {
            builder.UseHttpEndpoint(DaprOptions.HttpEndpoint);
        }

        if (!DaprOptions.GrpcEndpoint.IsNullOrWhiteSpace())
        {
            builder.UseGrpcEndpoint(DaprOptions.GrpcEndpoint);
        }

        var apiToken = DaprApiTokenProvider.GetDaprApiToken();
        if (!apiToken.IsNullOrWhiteSpace())
        {
            builder.UseDaprApiToken(apiToken);
        }

        builderAction?.Invoke(builder);

        return Task.FromResult(builder.Build());
    }

    public virtual async Task<HttpClient> CreateHttpClientAsync(
        string? appId = null,
        string? daprEndpoint = null,
        string? daprApiToken = null)
    {
        if(daprEndpoint.IsNullOrWhiteSpace() &&
           !DaprOptions.HttpEndpoint.IsNullOrWhiteSpace())
        {
            daprEndpoint = DaprOptions.HttpEndpoint;
        }

        var httpClient = DaprClient.CreateInvokeHttpClient(
            appId,
            daprEndpoint,
            daprApiToken ?? DaprApiTokenProvider.GetDaprApiToken()
        );

        AddHeaders(httpClient);

        var request = new HttpRequestMessage();
        await RemoteServiceHttpClientAuthenticator.Authenticate(
            new RemoteServiceHttpClientAuthenticateContext(
                httpClient,
                request,
                new RemoteServiceConfiguration("/"),
                string.Empty
            )
        );

        var bearerToken = request.Headers.Authorization?.Parameter;
        if (!bearerToken.IsNullOrWhiteSpace())
        {
            httpClient.SetBearerToken(bearerToken);
        }

        return httpClient;
    }

    protected virtual void AddHeaders(HttpClient httpClient)
    {
        //CorrelationId
        httpClient.DefaultRequestHeaders.Add(TiknasCorrelationIdOptions.Value.HttpHeaderName, CorrelationIdProvider.Get());

        //TenantId
         if (CurrentTenant.Id.HasValue)
         {
             //TODO: Use TiknasAspNetCoreMultiTenancyOptions to get the key
             httpClient.DefaultRequestHeaders.Add(TenantResolverConsts.DefaultTenantKey, CurrentTenant.Id.Value.ToString());
         }

        //Culture
        //TODO: Is that the way we want? Couldn't send the culture (not ui culture)
        var currentCulture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
        if (!currentCulture.IsNullOrEmpty())
        {
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(currentCulture));
        }

        //X-Requested-With
        httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
    }

    protected virtual JsonSerializerOptions CreateJsonSerializerOptions(TiknasSystemTextJsonSerializerOptions systemTextJsonSerializerOptions)
    {
        return new JsonSerializerOptions(systemTextJsonSerializerOptions.JsonSerializerOptions);
    }
}
