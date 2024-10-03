using Microsoft.AspNetCore.Mvc;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature(TiknasAspNetCoreMvcTestFeature1.Name)]
[Route("api/EnabledGlobalFeatureTestController-Test")]
public class EnabledGlobalFeatureTestController : TiknasController
{
    [HttpGet]
    [Route("TestMethod")]
    public string TestMethod()
    {
        return "TestMethod";
    }
}
