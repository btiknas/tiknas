using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Theming.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
