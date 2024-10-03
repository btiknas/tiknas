using System;
using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasApplicationPath;

public class TiknasApplicationPathViewComponent : TiknasViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        var applicationPath = ViewContext.HttpContext.Request.PathBase.Value;
        var model = new TiknasApplicationPathViewComponentModel
        {
            ApplicationPath = applicationPath == null ? "/" : applicationPath.EnsureEndsWith('/')
        };

        return View("~/Pages/Shared/Components/TiknasApplicationPath/Default.cshtml", model);
    }
}
