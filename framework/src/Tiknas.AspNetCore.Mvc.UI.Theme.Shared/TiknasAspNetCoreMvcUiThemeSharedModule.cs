using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Packages;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Widgets;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared;

[DependsOn(
    typeof(TiknasAspNetCoreMvcUiBootstrapModule),
    typeof(TiknasAspNetCoreMvcUiPackagesModule),
    typeof(TiknasAspNetCoreMvcUiWidgetsModule)
    )]
public class TiknasAspNetCoreMvcUiThemeSharedModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TiknasAspNetCoreMvcUiThemeSharedModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiThemeSharedModule>("Tiknas.AspNetCore.Mvc.UI.Theme.Shared");
        });
        
        Configure<TiknasBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(StandardBundles.Styles.Global, bundle => { bundle.AddContributors(typeof(SharedThemeGlobalStyleContributor)); });

            options
                .ScriptBundles
                .Add(StandardBundles.Scripts.Global, bundle => bundle.AddContributors(typeof(SharedThemeGlobalScriptContributor)));
        });
    }
}
