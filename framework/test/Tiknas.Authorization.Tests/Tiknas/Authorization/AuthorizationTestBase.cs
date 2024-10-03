using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Tiknas.Security.Claims;
using Tiknas.TestBase;

namespace Tiknas.Authorization;

public class AuthorizationTestBase : TiknasIntegratedTest<TiknasAuthorizationTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        var claims = new List<Claim>() {
                                new Claim(TiknasClaimTypes.UserName, "Douglas"),
                                new Claim(TiknasClaimTypes.UserId, "1fcf46b2-28c3-48d0-8bac-fa53268a2775"),
                                new Claim(TiknasClaimTypes.Role, "MyRole")
                            };

        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var principalAccessor = Substitute.For<ICurrentPrincipalAccessor>();
        principalAccessor.Principal.Returns(ci => claimsPrincipal);
        Thread.CurrentPrincipal = claimsPrincipal;
    }
}
