using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
