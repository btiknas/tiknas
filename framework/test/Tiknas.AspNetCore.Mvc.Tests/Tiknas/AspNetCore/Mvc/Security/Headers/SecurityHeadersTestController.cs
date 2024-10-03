using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.Security.Headers;

public class SecurityHeadersTestController : TiknasController
{
    public ActionResult Get()
    {
        return Content("OK");
    }
}
