using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Shouldly;
using Tiknas.AspNetCore.Mvc.Auditing;
using Tiknas.AspNetCore.Mvc.ExceptionHandling;
using Tiknas.AspNetCore.Mvc.Features;
using Tiknas.AspNetCore.Mvc.GlobalFeatures;
using Tiknas.AspNetCore.Mvc.Response;
using Tiknas.AspNetCore.Mvc.Uow;
using Tiknas.AspNetCore.Mvc.Validation;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.Filters;

[Route("api/enabled-features-test")]
public class EnabledTiknasFeaturesController : TiknasController, IRemoteService
{
    [HttpGet]
    public Task<List<string>> GetAsync()
    {
        var filters = HttpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>()
            .FilterDescriptors.Where(x => x.Filter is ServiceFilterAttribute)
            .Select(x => x.Filter.As<ServiceFilterAttribute>().ServiceType.FullName).ToList();

        return Task.FromResult(filters);
    }
}

[Route("api/disabled-features-test")]
[DisableTiknasFeatures]
public class DisabledTiknasFeaturesController : TiknasController, IRemoteService
{
    [HttpGet]
    public Task<List<string>> GetAsync()
    {
        var filters = HttpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>()
            .FilterDescriptors.Where(x => x.Filter is ServiceFilterAttribute)
            .Select(x => x.Filter.As<ServiceFilterAttribute>().ServiceType.FullName).ToList();

        return Task.FromResult(filters);
    }
}

public class MvcDisableTiknasFeaturesAttribute_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task Should_Disable_MVC_Filters()
    {
        var filters = await GetResponseAsObjectAsync<List<string>>("/api/enabled-features-test");
        filters.ShouldContain(typeof(GlobalFeatureActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasAuditActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasNoContentActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasFeatureActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasValidationActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasUowActionFilter).FullName);
        filters.ShouldContain(typeof(TiknasExceptionFilter).FullName);

        filters = await GetResponseAsObjectAsync<List<string>>("/api/disabled-features-test");
        filters.ShouldNotContain(typeof(GlobalFeatureActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasAuditActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasNoContentActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasFeatureActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasValidationActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasUowActionFilter).FullName);
        filters.ShouldNotContain(typeof(TiknasExceptionFilter).FullName);
    }

}
