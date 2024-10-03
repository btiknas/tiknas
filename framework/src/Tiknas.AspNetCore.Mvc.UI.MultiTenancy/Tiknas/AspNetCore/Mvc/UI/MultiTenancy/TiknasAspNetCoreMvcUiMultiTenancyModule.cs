using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.MultiTenancy;
using Tiknas.AspNetCore.Mvc.Localization;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.MultiTenancy.Localization;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.MultiTenancy;

[DependsOn(
    typeof(TiknasAspNetCoreMvcUiThemeSharedModule),
    typeof(TiknasAspNetCoreMultiTenancyModule)
    )]
public class TiknasAspNetCoreMvcUiMultiTenancyModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<TiknasMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(TiknasUiMultiTenancyResource),
                typeof(TiknasAspNetCoreMvcUiMultiTenancyModule).Assembly
            );
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TiknasAspNetCoreMvcUiMultiTenancyModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiMultiTenancyModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasUiMultiTenancyResource>("en")
                .AddVirtualJson("/Tiknas/AspNetCore/Mvc/UI/MultiTenancy/Localization");
        });

        Configure<TiknasBundlingOptions>(options =>
        {
            options.ScriptBundles
                .Get(StandardBundles.Scripts.Global)
                .AddFiles(
                    "/Pages/Tiknas/MultiTenancy/tenant-switch.js"
                );
        });
    }
}
