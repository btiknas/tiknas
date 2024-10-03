using Tiknas.Dapr;
using Tiknas.Modularity;

namespace Tiknas.DistributedLocking.Dapr;

[DependsOn(
    typeof(TiknasDistributedLockingAbstractionsModule),
    typeof(TiknasDaprModule))]
public class TiknasDistributedLockingDaprModule : TiknasModule
{
}