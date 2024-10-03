using Tiknas.AspNetCore.Components.Web.Theming;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.WebAssembly.Theming;

[DependsOn(
    typeof(TiknasAspNetCoreComponentsWebThemingModule),
    typeof(TiknasAspNetCoreComponentsWebAssemblyModule)
)]
public class TiknasAspNetCoreComponentsWebAssemblyThemingModule : TiknasModule
{

}
