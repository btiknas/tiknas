using Tiknas.BackgroundJobs;
using Tiknas.Emailing.Localization;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.Settings;
using Tiknas.TextTemplating;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Emailing;

[DependsOn(
    typeof(TiknasSettingsModule),
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasBackgroundJobsAbstractionsModule),
    typeof(TiknasLocalizationModule),
    typeof(TiknasTextTemplatingModule)
    )]
public class TiknasEmailingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasEmailingModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<EmailingResource>("en")
                .AddVirtualJson("/Tiknas/Emailing/Localization");
        });

        Configure<TiknasBackgroundJobOptions>(options =>
        {
            options.AddJob<BackgroundEmailSendingJob>();
        });
    }
}
