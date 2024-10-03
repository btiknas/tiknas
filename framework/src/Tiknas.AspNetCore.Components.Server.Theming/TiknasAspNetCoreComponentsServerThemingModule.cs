using Tiknas.AspNetCore.Components.Server.Theming.Bundling;
using Tiknas.AspNetCore.Components.Web.Theming;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Packages;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.Server.Theming;

[DependsOn(
    typeof(TiknasAspNetCoreComponentsServerModule),
    typeof(TiknasAspNetCoreMvcUiPackagesModule),
    typeof(TiknasAspNetCoreComponentsWebThemingModule),
    typeof(TiknasAspNetCoreMvcUiBundlingModule)
    )]
public class TiknasAspNetCoreComponentsServerThemingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorStandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                });
        });
    }
}
