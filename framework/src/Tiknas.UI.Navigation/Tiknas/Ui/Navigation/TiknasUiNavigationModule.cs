using Tiknas.Authorization;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.UI.Navigation.Localization.Resource;
using Tiknas.VirtualFileSystem;

namespace Tiknas.UI.Navigation;

[DependsOn(typeof(TiknasUiModule), typeof(TiknasAuthorizationModule), typeof(TiknasMultiTenancyModule))]
public class TiknasUiNavigationModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasUiNavigationModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasUiNavigationResource>("en")
                .AddVirtualJson("/Tiknas/Ui/Navigation/Localization/Resource");
        });

        Configure<TiknasNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DefaultMenuContributor());
        });
    }
}
