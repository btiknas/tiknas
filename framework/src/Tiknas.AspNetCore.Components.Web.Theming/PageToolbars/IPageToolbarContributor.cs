using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Theming.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}
