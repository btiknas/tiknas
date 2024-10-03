using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Tiknas.AspNetCore.Mvc.Localization;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.Controllers;

public class ReplaceBuiltInController_Tests : AspNetCoreMvcTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TiknasAspNetCoreMvcOptions>(options =>
        {
            options.ControllersToRemove.Add(typeof(TiknasLanguagesController));
        });
    }

    [Fact]
    public async Task Test()
    {
        var random = Guid.NewGuid().ToString("N");

        (await GetResponseAsObjectAsync<MyApplicationConfigurationDto>("api/tiknas/application-configuration?random=" + random)).Random.ShouldBe(random);
        (await GetResponseAsObjectAsync<MyApplicationLocalizationDto>("api/tiknas/application-localization?CultureName=en&random=" + random)).Random.ShouldBe(random);

        (await GetResponseAsync("Tiknas/Languages/Switch", HttpStatusCode.NotFound)).StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
