using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.App;

public class SimpleController : TiknasController
{
    public ActionResult Index()
    {
        return Content("Index-Result");
    }

    public ActionResult About()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }

    public ActionResult ExceptionOnRazor()
    {
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }
}
