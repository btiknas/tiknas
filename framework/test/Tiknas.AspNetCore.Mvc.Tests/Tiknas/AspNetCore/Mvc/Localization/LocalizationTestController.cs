using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.Localization;

[Route("api/LocalizationTestController")]
public class LocalizationTestController : TiknasController
{
    [HttpGet]
    public string Culture()
    {
        return CultureInfo.CurrentCulture.Name + ":" + CultureInfo.CurrentUICulture.Name;
    }
}
