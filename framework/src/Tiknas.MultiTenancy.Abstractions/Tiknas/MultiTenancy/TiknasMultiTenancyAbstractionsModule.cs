using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.MultiTenancy.Localization;
using Tiknas.VirtualFileSystem;

namespace Tiknas.MultiTenancy;

[DependsOn(
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasLocalizationModule)
)]
public class TiknasMultiTenancyAbstractionsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasMultiTenancyAbstractionsModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasMultiTenancyResource>("en")
                .AddVirtualJson("/Tiknas/MultiTenancy/Localization");
        });
    }
}
