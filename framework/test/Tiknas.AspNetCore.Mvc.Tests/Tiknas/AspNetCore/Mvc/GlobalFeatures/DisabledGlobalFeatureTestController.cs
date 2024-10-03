using Microsoft.AspNetCore.Mvc;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature("Not-Enabled-Feature")]
[Route("api/DisabledGlobalFeatureTestController-Test")]
public class DisabledGlobalFeatureTestController : TiknasController
{
    [HttpGet]
    [Route("TestMethod")]
    public string TestMethod()
    {
        return "TestMethod";
    }
}
