using Tiknas.AspNetCore.Components.Web.Security;
using Tiknas.BlazoriseUI;
using Tiknas.Modularity;
using Tiknas.UI.Navigation;

namespace Tiknas.AspNetCore.Components.Web.Theming;

[DependsOn(
    typeof(TiknasBlazoriseUIModule),
    typeof(TiknasUiNavigationModule)
    )]
public class TiknasAspNetCoreComponentsWebThemingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasDynamicLayoutComponentOptions>(options =>
        {
            options.Components.Add(typeof(TiknasAuthenticationState), null);
        });
    }
}
