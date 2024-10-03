using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.ProxyScripting;

public class TiknasServiceProxiesController_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task GetAll()
    {
        var script = await GetResponseAsStringAsync("/Tiknas/ServiceProxyScript?minify=true");
        script.Length.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetAllWithMinify()
    {
        GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>().Value.MinifyGeneratedScript = false;
        var script = await GetResponseAsStringAsync("/Tiknas/ServiceProxyScript");

        GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>().Value.MinifyGeneratedScript = true;
        var minifyScript = await GetResponseAsStringAsync("/Tiknas/ServiceProxyScript?minify=true");

        script.Length.ShouldBeGreaterThan(0);
        minifyScript.Length.ShouldBeGreaterThan(0);
        minifyScript.Length.ShouldBeLessThan(script.Length);
    }
}
