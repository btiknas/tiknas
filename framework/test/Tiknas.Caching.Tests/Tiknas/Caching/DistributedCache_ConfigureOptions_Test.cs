using Microsoft.Extensions.Caching.Distributed;
using Shouldly;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Caching;

public class DistributedCache_ConfigureOptions_Test : TiknasIntegratedTest<TiknasCachingTestModule>
{
    [Fact]
    public void Configure_TiknasCacheOptions()
    {
        var personCache = GetRequiredService<IDistributedCache<Sail.Testing.Caching.PersonCacheItem>>();
        GetDefaultCachingOptions(personCache).SlidingExpiration.ShouldBeNull();
        GetDefaultCachingOptions(personCache).AbsoluteExpiration.ShouldBe(new DateTime(2099, 1, 1, 12, 0, 0));
    }

    [Fact]
    public async Task Default_TiknasCacheOptions_Should_Be_20_Mins()
    {
        var personCache = GetRequiredService<IDistributedCache<PersonCacheItem>>();

        var cacheKey = Guid.NewGuid().ToString();

        //Get (not exists yet)
        var cacheItem = await personCache.GetAsync(cacheKey);
        cacheItem.ShouldBeNull();

        GetDefaultCachingOptions(personCache).SlidingExpiration.ShouldBe(TimeSpan.FromMinutes(20));

    }
    private static DistributedCacheEntryOptions GetDefaultCachingOptions(object instance)
    {
        var internalCacheProperty = instance
            .GetType()
            .GetProperty("InternalCache", BindingFlags.Instance | BindingFlags.Public);

        if (internalCacheProperty != null)
        {
            instance = internalCacheProperty.GetValue(instance);
        }
        
        var defaultOptionsField = instance
            .GetType()
            .GetField("DefaultCacheOptions", BindingFlags.Instance | BindingFlags.NonPublic);
        
        return (DistributedCacheEntryOptions)defaultOptionsField.GetValue(instance);
    }
}
