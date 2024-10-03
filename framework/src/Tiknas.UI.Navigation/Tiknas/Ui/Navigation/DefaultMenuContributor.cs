using System.Threading.Tasks;
using Tiknas.UI.Navigation.Localization.Resource;

namespace Tiknas.UI.Navigation;

public class DefaultMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        Configure(context);
        return Task.CompletedTask;
    }

    protected virtual void Configure(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<TiknasUiNavigationResource>();

        if (context.Menu.Name == StandardMenus.Main)
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    DefaultMenuNames.Application.Main.Administration,
                    l["Menu:Administration"],
                    icon: "fa fa-wrench"
                )
            );
        }
    }
}
