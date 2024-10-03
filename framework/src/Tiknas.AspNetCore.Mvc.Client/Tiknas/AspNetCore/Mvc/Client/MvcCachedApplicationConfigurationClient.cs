using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ClientProxies;
using Tiknas.Caching;
using Tiknas.DependencyInjection;
using Tiknas.Threading;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Mvc.Client;

public class MvcCachedApplicationConfigurationClient : ICachedApplicationConfigurationClient, ITransientDependency
{
    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected TiknasApplicationConfigurationClientProxy ApplicationConfigurationAppService { get; }
    protected TiknasApplicationLocalizationClientProxy ApplicationLocalizationClientProxy { get; }
    protected ICurrentUser CurrentUser { get; }
    protected IDistributedCache<ApplicationConfigurationDto> Cache { get; }
    protected TiknasAspNetCoreMvcClientCacheOptions Options { get; }

    public MvcCachedApplicationConfigurationClient(
        IDistributedCache<ApplicationConfigurationDto> cache,
        TiknasApplicationConfigurationClientProxy applicationConfigurationAppService,
        ICurrentUser currentUser,
        IHttpContextAccessor httpContextAccessor,
        TiknasApplicationLocalizationClientProxy applicationLocalizationClientProxy,
        IOptions<TiknasAspNetCoreMvcClientCacheOptions> options)
    {
        ApplicationConfigurationAppService = applicationConfigurationAppService;
        CurrentUser = currentUser;
        HttpContextAccessor = httpContextAccessor;
        ApplicationLocalizationClientProxy = applicationLocalizationClientProxy;
        Options = options.Value;
        Cache = cache;
    }

    public async Task<ApplicationConfigurationDto> GetAsync()
    {
        var cacheKey = CreateCacheKey();
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is ApplicationConfigurationDto configuration)
        {
            return configuration;
        }

        configuration = (await Cache.GetOrAddAsync(
            cacheKey,
            async () => await GetRemoteConfigurationAsync(),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = Options.ApplicationConfigurationDtoCacheAbsoluteExpiration
            }
        ))!;

        if (httpContext != null)
        {
            httpContext.Items[cacheKey] = configuration;
        }

        return configuration;
    }

    private async Task<ApplicationConfigurationDto> GetRemoteConfigurationAsync()
    {
        var config = await ApplicationConfigurationAppService.GetAsync(
            new ApplicationConfigurationRequestOptions
            {
                IncludeLocalizationResources = false
            }
        );

        var localizationDto = await ApplicationLocalizationClientProxy.GetAsync(
            new ApplicationLocalizationRequestDto {
                CultureName = config.Localization.CurrentCulture.Name,
                OnlyDynamics = true
            }
        );

        config.Localization.Resources = localizationDto.Resources;

        return config;
    }

    public ApplicationConfigurationDto Get()
    {
        var cacheKey = CreateCacheKey();
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is ApplicationConfigurationDto configuration)
        {
            return configuration;
        }

        return AsyncHelper.RunSync(GetAsync);
    }

    protected virtual string CreateCacheKey()
    {
        return MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser);
    }
}
