using System;
using System.Collections.Generic;

namespace Tiknas.Security.Claims;

[Serializable]
public class TiknasDynamicClaimCacheItem
{
    public List<TiknasDynamicClaim> Claims { get; set; }

    public TiknasDynamicClaimCacheItem()
    {
        Claims = new List<TiknasDynamicClaim>();
    }

    public TiknasDynamicClaimCacheItem(List<TiknasDynamicClaim> claims)
    {
        Claims = claims;
    }

    public static string CalculateCacheKey(Guid userId, Guid? tenantId)
    {
        return $"{tenantId}-{userId}";
    }
}
