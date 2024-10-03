using Tiknas.Modularity;

namespace Tiknas.Threading;

[DependsOn(
    typeof(TiknasThreadingModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasThreadingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
