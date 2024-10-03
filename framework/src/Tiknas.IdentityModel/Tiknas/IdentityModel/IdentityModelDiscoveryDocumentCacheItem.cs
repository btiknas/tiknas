using System;
using Tiknas.MultiTenancy;

namespace Tiknas.IdentityModel;

[Serializable]
[IgnoreMultiTenancy]
public class IdentityModelDiscoveryDocumentCacheItem
{
    public string TokenEndpoint { get; set; } = default!;

    public string DeviceAuthorizationEndpoint { get; set; } = default!;

    public IdentityModelDiscoveryDocumentCacheItem()
    {

    }

    public IdentityModelDiscoveryDocumentCacheItem(string tokenEndpoint, string deviceAuthorizationEndpoint)
    {
        TokenEndpoint = tokenEndpoint;
        DeviceAuthorizationEndpoint = deviceAuthorizationEndpoint;
    }

    public static string CalculateCacheKey(IdentityClientConfiguration configuration)
    {
        return configuration.Authority.ToLower().ToSha256();
    }
}
