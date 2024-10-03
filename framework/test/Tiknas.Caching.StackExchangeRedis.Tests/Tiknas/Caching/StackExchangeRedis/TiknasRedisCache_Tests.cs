using Microsoft.Extensions.Caching.Distributed;
using Shouldly;
using Xunit;

namespace Tiknas.Caching.StackExchangeRedis;

public class TiknasRedisCache_Tests : TiknasCachingStackExchangeRedisTestBase
{
    private readonly IDistributedCache _distributedCache;

    public TiknasRedisCache_Tests()
    {
        _distributedCache = GetRequiredService<IDistributedCache>();
    }

    [Fact]
    public void Should_Replace_RedisCache()
    {
        (_distributedCache is TiknasRedisCache).ShouldBeTrue();
    }
}
