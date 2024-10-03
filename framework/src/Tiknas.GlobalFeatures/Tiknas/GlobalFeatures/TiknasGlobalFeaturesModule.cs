using Microsoft.Extensions.DependencyInjection;
using Tiknas.Authorization;
using Tiknas.GlobalFeatures.Localization;
using Tiknas.Localization;
using Tiknas.Localization.ExceptionHandling;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.GlobalFeatures;

[DependsOn(
    typeof(TiknasLocalizationModule),
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasAuthorizationAbstractionsModule)
)]
public class TiknasGlobalFeaturesModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(GlobalFeatureInterceptorRegistrar.RegisterIfNeeded);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasGlobalFeatureResource>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasGlobalFeatureResource>("en")
                .AddVirtualJson("/Tiknas/GlobalFeatures/Localization");
        });

        Configure<TiknasExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Tiknas.GlobalFeature", typeof(TiknasGlobalFeatureResource));
        });
    }
}
