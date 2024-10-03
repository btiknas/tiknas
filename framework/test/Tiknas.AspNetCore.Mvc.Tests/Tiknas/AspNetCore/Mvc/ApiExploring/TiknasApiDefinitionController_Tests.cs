using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Http.Modeling;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.ApiExploring;

public class TiknasApiDefinitionController_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task GetAsync()
    {
        var model = await GetResponseAsObjectAsync<ApplicationApiDescriptionModel>("/api/tiknas/api-definition");
        model.ShouldNotBeNull();
        model.Types.IsNullOrEmpty().ShouldBeTrue();
    }

    [Fact]
    public async Task GetAsync_IncludeTypes()
    {
        var model = await GetResponseAsObjectAsync<ApplicationApiDescriptionModel>("/api/tiknas/api-definition?includeTypes=true");
        model.ShouldNotBeNull();
        model.Types.IsNullOrEmpty().ShouldBeFalse();
    }
}
