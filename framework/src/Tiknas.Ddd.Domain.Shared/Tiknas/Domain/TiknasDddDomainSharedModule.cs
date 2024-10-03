using Tiknas.EventBus.Abstractions;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.Domain;

[DependsOn(
    typeof(TiknasMultiTenancyAbstractionsModule),
    typeof(TiknasEventBusAbstractionsModule)
)]
public class TiknasDddDomainSharedModule : TiknasModule
{

}
