using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc;
using Tiknas.Tracing;

namespace Tiknas.AspNetCore.CorrelationIdProvider;

[Route("api/correlation")]
public class CorrelationIdProviderController : TiknasController
{
    [HttpGet]
    [Route("get")]
    public string Get()
    {
        return this.HttpContext.RequestServices.GetRequiredService<ICorrelationIdProvider>().Get();
    }
}
