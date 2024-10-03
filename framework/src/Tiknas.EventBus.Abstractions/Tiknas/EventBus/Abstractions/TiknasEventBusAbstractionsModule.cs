using Tiknas.Modularity;
using Tiknas.ObjectExtending;

namespace Tiknas.EventBus.Abstractions;

[DependsOn(
    typeof(TiknasObjectExtendingModule)
)]
public class TiknasEventBusAbstractionsModule : TiknasModule
{

}
