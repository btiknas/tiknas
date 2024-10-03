using Tiknas.AspNetCore.Components.Web.Theming;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.MauiBlazor.Theming;

[DependsOn(
    typeof(TiknasAspNetCoreComponentsWebThemingModule),
    typeof(TiknasAspNetCoreComponentsMauiBlazorModule)
)]
public class TiknasAspNetCoreComponentsMauiBlazorThemingModule : TiknasModule
{

}
