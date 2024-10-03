using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.DistributedLocking;

[DependsOn(
    typeof(TiknasDistributedLockingAbstractionsModule),
    typeof(TiknasThreadingModule)
    )]
public class TiknasDistributedLockingModule : TiknasModule
{
}
