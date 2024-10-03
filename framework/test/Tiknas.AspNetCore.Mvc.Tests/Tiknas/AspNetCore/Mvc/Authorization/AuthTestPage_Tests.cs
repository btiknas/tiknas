using System.Security.Claims;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.MemoryDb;
using Tiknas.Modularity;
using Tiknas.Security.Claims;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.Authorization;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasMemoryDbTestModule),
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasAutofacModule)
)]
public class AuthTestPage_Tests : AspNetCoreMvcTestBase
{
    private readonly FakeUserClaims _fakeRequiredService;

    public AuthTestPage_Tests()
    {
        _fakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    [Fact]
    public async Task Should_Call_Simple_Authorized_Method_With_Authenticated_User()
    {
        _fakeRequiredService.Claims.AddRange(new[]
        {
                new Claim(TiknasClaimTypes.UserId, AuthTestController.FakeUserId.ToString())
            });

        var result = await GetResponseAsStringAsync("/Authorization/AuthTestPage");
        result.ShouldBe("OK");
    }
}
