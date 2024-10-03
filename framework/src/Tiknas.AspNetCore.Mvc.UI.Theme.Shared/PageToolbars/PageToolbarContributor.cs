using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public abstract class PageToolbarContributor : IPageToolbarContributor
{
    public abstract Task ContributeAsync(PageToolbarContributionContext context);
}
