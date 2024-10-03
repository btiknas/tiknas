using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pages.Tiknas.MultiTenancy.ClientProxies;
using Tiknas.Caching;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;
using Tiknas.Threading;

namespace Tiknas.AspNetCore.Mvc.Client;

public class MvcRemoteTenantStore : ITenantStore, ITransientDependency
{
    protected TiknasTenantClientProxy TenantAppService { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected IDistributedCache<TenantConfigurationCacheItem> Cache { get; }
    protected TiknasAspNetCoreMvcClientCacheOptions Options { get; }

    public MvcRemoteTenantStore(
        TiknasTenantClientProxy tenantAppService,
        IHttpContextAccessor httpContextAccessor,
        IDistributedCache<TenantConfigurationCacheItem> cache,
        IOptions<TiknasAspNetCoreMvcClientCacheOptions> options)
    {
        TenantAppService = tenantAppService;
        HttpContextAccessor = httpContextAccessor;
        Cache = cache;
        Options = options.Value;
    }

    public async Task<TenantConfiguration?> FindAsync(string normalizedName)
    {
        var cacheKey = TenantConfigurationCacheItem.CalculateCacheKey(normalizedName);
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is TenantConfigurationCacheItem tenantConfigurationInHttpContext)
        {
            return tenantConfigurationInHttpContext?.Value;
        }

        var tenantConfiguration = await Cache.GetAsync(cacheKey);
        if (tenantConfiguration?.Value == null)
        {
            var tenant = await TenantAppService.FindTenantByNameAsync(normalizedName);
            tenantConfiguration = await Cache.GetAsync(cacheKey);
        }

        if (httpContext != null)
        {
            httpContext.Items[cacheKey] = tenantConfiguration;
        }

        return tenantConfiguration?.Value;
    }

    public async Task<TenantConfiguration?> FindAsync(Guid id)
    {
        var cacheKey = TenantConfigurationCacheItem.CalculateCacheKey(id);
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is TenantConfigurationCacheItem tenantConfigurationInHttpContext)
        {
            return tenantConfigurationInHttpContext?.Value;
        }

        var tenantConfiguration = await Cache.GetAsync(cacheKey);
        if (tenantConfiguration?.Value == null)
        {
            await TenantAppService.FindTenantByIdAsync(id);
            tenantConfiguration = await Cache.GetAsync(cacheKey);
        }

        if (httpContext != null)
        {
            httpContext.Items[cacheKey] = tenantConfiguration;
        }

        return tenantConfiguration?.Value;
    }

    public Task<IReadOnlyList<TenantConfiguration>> GetListAsync(bool includeDetails = false)
    {
        return Task.FromResult<IReadOnlyList<TenantConfiguration>>(Array.Empty<TenantConfiguration>());
    }

    public TenantConfiguration? Find(string normalizedName)
    {
        var cacheKey = TenantConfigurationCacheItem.CalculateCacheKey(normalizedName);
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is TenantConfigurationCacheItem tenantConfigurationInHttpContext)
        {
            return tenantConfigurationInHttpContext?.Value;
        }

        var tenantConfiguration = Cache.Get(cacheKey);
        if (tenantConfiguration?.Value == null)
        {
            AsyncHelper.RunSync(async () => await TenantAppService.FindTenantByNameAsync(normalizedName));
            tenantConfiguration = Cache.Get(cacheKey);
        }

        if (httpContext != null)
        {
            httpContext.Items[cacheKey] = tenantConfiguration;
        }

        return tenantConfiguration?.Value;
    }

    public TenantConfiguration? Find(Guid id)
    {
        var cacheKey = TenantConfigurationCacheItem.CalculateCacheKey(id);
        var httpContext = HttpContextAccessor?.HttpContext;

        if (httpContext != null && httpContext.Items[cacheKey] is TenantConfigurationCacheItem tenantConfigurationInHttpContext)
        {
            return tenantConfigurationInHttpContext?.Value;
        }

        var tenantConfiguration = Cache.Get(cacheKey);
        if (tenantConfiguration?.Value == null)
        {
            AsyncHelper.RunSync(async () => await TenantAppService.FindTenantByIdAsync(id));
            tenantConfiguration = Cache.Get(cacheKey);
        }

        if (httpContext != null)
        {
            httpContext.Items[cacheKey] = tenantConfiguration;
        }

        return tenantConfiguration?.Value;
    }
}
