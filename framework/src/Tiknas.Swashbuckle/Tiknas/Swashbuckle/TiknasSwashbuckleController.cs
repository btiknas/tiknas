using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.AntiForgery;
using Tiknas.Auditing;

namespace Tiknas.Swashbuckle;

[Area("Tiknas")]
[Route("Tiknas/Swashbuckle/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class TiknasSwashbuckleController : TiknasController
{
    protected readonly ITiknasAntiForgeryManager AntiForgeryManager;

    public TiknasSwashbuckleController(ITiknasAntiForgeryManager antiForgeryManager)
    {
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    public virtual void SetCsrfCookie()
    {
        AntiForgeryManager.SetCookie();
    }
}
