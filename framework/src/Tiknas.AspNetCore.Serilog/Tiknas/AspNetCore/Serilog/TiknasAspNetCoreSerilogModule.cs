using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.Serilog;

[DependsOn(
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasAspNetCoreModule)
)]
public class TiknasAspNetCoreSerilogModule : TiknasModule
{
}
