using Tiknas.Localization.Resources.TiknasLocalization;
using Tiknas.Modularity;
using Tiknas.Settings;
using Tiknas.Threading;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Localization;

[DependsOn(
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasSettingsModule),
    typeof(TiknasLocalizationAbstractionsModule),
    typeof(TiknasThreadingModule)
    )]
public class TiknasLocalizationModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        TiknasStringLocalizerFactory.Replace(context.Services);

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasLocalizationModule>("Tiknas", "Tiknas");
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<DefaultResource>("en");

            options
                .Resources
                .Add<TiknasLocalizationResource>("en")
                .AddVirtualJson("/Localization/Resources/TiknasLocalization");
        });
    }
}
