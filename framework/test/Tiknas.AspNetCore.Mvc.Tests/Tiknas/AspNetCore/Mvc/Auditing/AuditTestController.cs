using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tiknas.Auditing;

namespace Tiknas.AspNetCore.Mvc.Auditing;

[Route("api/audit-test")]
public class AuditTestController : TiknasController
{
    private readonly TiknasAuditingOptions _options;

    public AuditTestController(IOptions<TiknasAuditingOptions> options)
    {
        _options = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [Route("audit-success")]
    public IActionResult AuditSuccessForGetRequests()
    {
        return Ok();
    }

    [Route("audit-fail")]
    public IActionResult AuditFailForGetRequests()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    [Route("audit-fail-object")]
    public object AuditFailForGetRequestsReturningObject()
    {
        throw new UserFriendlyException("Exception occurred!");
    }

    [HttpGet]
    [Route("audit-activate-failed")]
    public IActionResult AuditActivateFailed([FromServices] TiknasAuditingOptions options)
    {
        return Ok();
    }
}
