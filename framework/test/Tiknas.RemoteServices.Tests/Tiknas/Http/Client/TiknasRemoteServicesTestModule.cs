using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.RemoteServices;

namespace Tiknas.Http.Client;

[DependsOn(
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasRemoteServicesModule)
)]
public class TiknasRemoteServicesTestModule: TiknasModule
{
}