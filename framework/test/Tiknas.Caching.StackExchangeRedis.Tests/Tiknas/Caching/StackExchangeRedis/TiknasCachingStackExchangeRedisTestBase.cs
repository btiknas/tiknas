using Tiknas.TestBase;

namespace Tiknas.Caching.StackExchangeRedis;

public abstract class TiknasCachingStackExchangeRedisTestBase : TiknasIntegratedTest<TiknasCachingStackExchangeRedisTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
