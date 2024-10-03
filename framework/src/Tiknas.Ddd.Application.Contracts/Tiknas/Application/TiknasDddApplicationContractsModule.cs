using Tiknas.Application.Localization.Resources.TiknasDdd;
using Tiknas.Auditing;
using Tiknas.Data;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Application;

[DependsOn(
    typeof(TiknasLocalizationModule),
    typeof(TiknasAuditingContractsModule),
    typeof(TiknasDataModule)
    )]
public class TiknasDddApplicationContractsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasDddApplicationContractsModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasDddApplicationContractsResource>("en")
                .AddVirtualJson("/Tiknas/Application/Localization/Resources/TiknasDdd");
        });
    }
}
