using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.Caching;
using Tiknas.Http.Client;
using Tiknas.Http.Client.Authentication;
using Tiknas.Security.Claims;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Mvc.Client;

public class RemoteDynamicClaimsPrincipalContributorCache : RemoteDynamicClaimsPrincipalContributorCacheBase<RemoteDynamicClaimsPrincipalContributorCache>
{
    public const string HttpClientName = nameof(RemoteDynamicClaimsPrincipalContributorCache);

    protected IDistributedCache<TiknasDynamicClaimCacheItem> Cache { get; }
    protected IHttpClientFactory HttpClientFactory { get; }
    protected IRemoteServiceHttpClientAuthenticator HttpClientAuthenticator { get; }
    protected IDistributedCache<ApplicationConfigurationDto> ApplicationConfigurationDtoCache { get; }
    protected ICurrentUser CurrentUser { get; }

    public RemoteDynamicClaimsPrincipalContributorCache(
        IDistributedCache<TiknasDynamicClaimCacheItem> cache,
        IHttpClientFactory httpClientFactory,
        IOptions<TiknasClaimsPrincipalFactoryOptions> tiknasClaimsPrincipalFactoryOptions,
        IRemoteServiceHttpClientAuthenticator httpClientAuthenticator,
        IDistributedCache<ApplicationConfigurationDto> applicationConfigurationDtoCache,
        ICurrentUser currentUser)
        : base(tiknasClaimsPrincipalFactoryOptions)
    {
        Cache = cache;
        HttpClientFactory = httpClientFactory;
        HttpClientAuthenticator = httpClientAuthenticator;
        ApplicationConfigurationDtoCache = applicationConfigurationDtoCache;
        CurrentUser = currentUser;
    }

    protected async override Task<TiknasDynamicClaimCacheItem?> GetCacheAsync(Guid userId, Guid? tenantId = null)
    {
        return await Cache.GetAsync(TiknasDynamicClaimCacheItem.CalculateCacheKey(userId, tenantId));
    }

    protected async override Task RefreshAsync(Guid userId, Guid? tenantId = null)
    {
        try
        {
            var client = HttpClientFactory.CreateClient(HttpClientName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, TiknasClaimsPrincipalFactoryOptions.Value.RemoteRefreshUrl);
            await HttpClientAuthenticator.Authenticate(new RemoteServiceHttpClientAuthenticateContext(client, requestMessage, new RemoteServiceConfiguration("/"), string.Empty));
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, $"Failed to refresh remote claims for user: {userId}");
            await ApplicationConfigurationDtoCache.RemoveAsync(MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser));
            throw;
        }
    }
}
