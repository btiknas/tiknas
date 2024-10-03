using Tiknas.Dapr;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.Dapr;

[DependsOn(
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasDaprModule)
)]
public class TiknasAspNetCoreMvcDaprModule : TiknasModule
{

}
