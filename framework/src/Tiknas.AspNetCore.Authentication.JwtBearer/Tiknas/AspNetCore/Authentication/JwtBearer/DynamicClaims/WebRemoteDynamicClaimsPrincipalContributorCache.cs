using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.Caching;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Authentication.JwtBearer.DynamicClaims;

public class WebRemoteDynamicClaimsPrincipalContributorCache : RemoteDynamicClaimsPrincipalContributorCacheBase<WebRemoteDynamicClaimsPrincipalContributorCache>
{
    public const string HttpClientName = nameof(WebRemoteDynamicClaimsPrincipalContributorCache);

    protected IDistributedCache<TiknasDynamicClaimCacheItem> Cache { get; }
    protected IHttpClientFactory HttpClientFactory { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected IOptions<WebRemoteDynamicClaimsPrincipalContributorOptions> Options { get; }

    public WebRemoteDynamicClaimsPrincipalContributorCache(
        IDistributedCache<TiknasDynamicClaimCacheItem> cache,
        IHttpClientFactory httpClientFactory,
        IOptions<TiknasClaimsPrincipalFactoryOptions> tiknasClaimsPrincipalFactoryOptions,
        IHttpContextAccessor httpContextAccessor,
        IOptions<WebRemoteDynamicClaimsPrincipalContributorOptions> options)
        : base(tiknasClaimsPrincipalFactoryOptions)
    {
        Cache = cache;
        HttpClientFactory = httpClientFactory;
        HttpContextAccessor = httpContextAccessor;
        Options = options;
    }

    protected async override Task<TiknasDynamicClaimCacheItem?> GetCacheAsync(Guid userId, Guid? tenantId = null)
    {
        return await Cache.GetAsync(TiknasDynamicClaimCacheItem.CalculateCacheKey(userId, tenantId));
    }

    protected async override Task RefreshAsync(Guid userId, Guid? tenantId = null)
    {
        try
        {
            if (HttpContextAccessor.HttpContext == null)
            {
                throw new TiknasException($"Failed to refresh remote claims for user: {userId} - HttpContext is null!");
            }

            var authenticateResult = await HttpContextAccessor.HttpContext.AuthenticateAsync(Options.Value.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                throw new TiknasException($"Failed to refresh remote claims for user: {userId} - authentication failed!");
            }

            var accessToken = authenticateResult.Properties?.GetTokenValue("access_token");
            if (accessToken.IsNullOrWhiteSpace())
            {
                throw new TiknasException($"Failed to refresh remote claims for user: {userId} - access_token is null or empty!");
            }

            var client = HttpClientFactory.CreateClient(HttpClientName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, TiknasClaimsPrincipalFactoryOptions.Value.RemoteRefreshUrl);
            requestMessage.SetBearerToken(accessToken);
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, $"Failed to refresh remote claims for user: {userId}");
            throw;
        }
    }
}
