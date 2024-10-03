using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Caching.StackExchangeRedis;

[DependsOn(
    typeof(TiknasCachingStackExchangeRedisModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasCachingStackExchangeRedisTestModule : TiknasModule
{

}
