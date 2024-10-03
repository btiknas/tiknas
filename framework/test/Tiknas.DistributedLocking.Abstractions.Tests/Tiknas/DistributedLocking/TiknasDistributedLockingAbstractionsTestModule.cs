using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.DistributedLocking;

[DependsOn(
    typeof(TiknasTestBaseModule),
    typeof(TiknasDistributedLockingAbstractionsModule),
    typeof(TiknasAutofacModule)
)]
public class TiknasDistributedLockingAbstractionsTestModule : TiknasModule
{

}
