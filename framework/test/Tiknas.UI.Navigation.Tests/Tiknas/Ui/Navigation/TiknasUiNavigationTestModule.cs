using Tiknas.Authorization;
using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.UI.Navigation;

[DependsOn(typeof(TiknasUiNavigationModule))]
[DependsOn(typeof(TiknasAuthorizationModule))]
[DependsOn(typeof(TiknasAutofacModule))]
public class TiknasUiNavigationTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor1());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor2());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor3());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor4());

            options.MainMenuNames.Add(MenuManager_Tests.TestMenuContributor3.MenuName);
        });
    }
}
