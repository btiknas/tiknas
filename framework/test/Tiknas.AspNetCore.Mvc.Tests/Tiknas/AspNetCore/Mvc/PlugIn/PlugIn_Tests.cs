using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.PlugIn;

public class PlugIn_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task Get_PlugIn_Views()
    {
        var page = await GetResponseAsStringAsync(
            "/Index"
        );

        page.ShouldContain("Welcome to my plug-in page");
    }
}
