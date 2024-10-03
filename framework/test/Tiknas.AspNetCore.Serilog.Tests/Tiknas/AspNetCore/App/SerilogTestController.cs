using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tiknas.AspNetCore.Mvc;
using Tiknas.Tracing;

namespace Tiknas.AspNetCore.App;

public class SerilogTestController : TiknasController
{
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public SerilogTestController(ICorrelationIdProvider correlationIdProvider)
    {
        _correlationIdProvider = correlationIdProvider;
    }

    public ActionResult Index()
    {
        return Content("Index-Result");
    }

    public ActionResult CorrelationId()
    {
        return Content(_correlationIdProvider.Get());
    }
}
