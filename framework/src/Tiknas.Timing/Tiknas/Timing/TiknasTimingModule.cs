using Tiknas.Localization;
using Tiknas.Localization.Resources.TiknasLocalization;
using Tiknas.Modularity;
using Tiknas.Settings;
using Tiknas.Timing.Localization.Resources.TiknasTiming;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Timing;

[DependsOn(
    typeof(TiknasLocalizationModule),
    typeof(TiknasSettingsModule)
    )]
public class TiknasTimingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasTimingModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options
                .Resources
                .Add<TiknasTimingResource>("en")
                .AddVirtualJson("/Tiknas/Timing/Localization");
        });
    }
}
