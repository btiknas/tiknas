using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasPageSearchBox;

public class TiknasPageSearchBoxViewComponent : TiknasViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        return View("~/Pages/Shared/Components/TiknasPageSearchBox/Default.cshtml");
    }
}
