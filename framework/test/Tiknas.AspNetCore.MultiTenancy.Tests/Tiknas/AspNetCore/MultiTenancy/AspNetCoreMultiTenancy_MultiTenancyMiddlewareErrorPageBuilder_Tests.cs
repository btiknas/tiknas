using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.Http;
using Xunit;

namespace Tiknas.AspNetCore.MultiTenancy;

public class AspNetCoreMultiTenancy_MultiTenancyMiddlewareErrorPageBuilder_Tests : AspNetCoreMultiTenancyTestBase
{
    private readonly TiknasAspNetCoreMultiTenancyOptions _options;

    public AspNetCoreMultiTenancy_MultiTenancyMiddlewareErrorPageBuilder_Tests()
    {
        _options = ServiceProvider.GetRequiredService<IOptions<TiknasAspNetCoreMultiTenancyOptions>>().Value;
    }

    [Fact]
    public async Task MultiTenancyMiddlewareErrorPageBuilder()
    {
        var result = await GetResponseAsStringAsync($"http://tiknas.io?{_options.TenantKey}=<script>alert(hi)</script>", HttpStatusCode.NotFound);
        result.ShouldNotContain("<script>alert(hi)</script>");
    }

    [Fact]
    public async Task MultiTenancyMiddlewareErrorPageBuilder_Ajax_Test()
    {
        using (var response = await GetResponseAsync($"http://tiknas.io?{_options.TenantKey}=tiknasio", HttpStatusCode.NotFound, xmlHttpRequest: true))
        {
            var result = await response.Content.ReadAsStringAsync();
            var error = JsonSerializer.Deserialize<RemoteServiceErrorResponse>(result, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            error.Error.ShouldNotBeNull();
            error.Error.Message.ShouldBe("Tenant not found!");
            error.Error.Details.ShouldBe("There is no tenant with the tenant id or name: tiknasio");
        }
    }
}
