using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasPageToolbar;

public class TiknasPageToolbarViewComponent : TiknasViewComponent
{
    protected IPageToolbarManager ToolbarManager { get; }

    public TiknasPageToolbarViewComponent(IPageToolbarManager toolbarManager)
    {
        ToolbarManager = toolbarManager;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string pageName)
    {
        var items = await ToolbarManager.GetItemsAsync(pageName);
        return View("~/Pages/Shared/Components/TiknasPageToolbar/Default.cshtml", items);
    }
}
