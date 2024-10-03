using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}
