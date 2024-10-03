using Tiknas.AspNetCore.Components.WebAssembly;
using Tiknas.Modularity;

namespace Tiknas.Autofac.WebAssembly;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasAspNetCoreComponentsWebAssemblyModule)
    )]
public class TiknasAutofacWebAssemblyModule : TiknasModule
{

}
