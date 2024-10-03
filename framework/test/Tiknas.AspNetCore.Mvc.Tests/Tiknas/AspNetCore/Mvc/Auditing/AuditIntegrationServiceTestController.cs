using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.Auditing;

[Route("integration-api/audit-test")]
[IntegrationService]
public class AuditIntegrationServiceTestController : TiknasController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}