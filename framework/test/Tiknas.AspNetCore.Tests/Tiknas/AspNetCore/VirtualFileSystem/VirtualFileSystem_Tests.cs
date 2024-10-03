using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tiknas.AspNetCore.VirtualFileSystem;

public class VirtualFileSystem_Tests : TiknasAspNetCoreTestBase
{
    [Fact]
    public async Task Get_Virtual_File()
    {
        var result = await GetResponseAsStringAsync(
            "/SampleFiles/test1.js"
        );

        result.ShouldBe("test1.js-content");
    }
}
