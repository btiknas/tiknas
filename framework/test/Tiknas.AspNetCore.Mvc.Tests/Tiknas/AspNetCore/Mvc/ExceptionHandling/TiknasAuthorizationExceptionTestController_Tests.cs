using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Shouldly;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.ExceptionHandling;
using Tiknas.Security.Claims;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.ExceptionHandling;

public class TiknasAuthorizationExceptionTestController_Tests : AspNetCoreMvcTestBase
{
    protected IExceptionSubscriber FakeExceptionSubscriber;

    protected FakeUserClaims FakeRequiredService;

    public TiknasAuthorizationExceptionTestController_Tests()
    {
        FakeRequiredService = GetRequiredService<FakeUserClaims>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        FakeExceptionSubscriber = Substitute.For<IExceptionSubscriber>();

        services.AddSingleton(FakeExceptionSubscriber);

        services.Configure<TiknasAuthorizationExceptionHandlerOptions>(options =>
        {
            options.AuthenticationScheme = "Cookie";
        });
    }

    [Fact]
    public virtual async Task Should_Handle_By_Cookie_AuthenticationScheme_For_TiknasAuthorizationException()
    {
        var result = await GetResponseAsync("/api/exception-test/TiknasAuthorizationException", HttpStatusCode.Redirect);
        result.Headers.Location.ToString().ShouldContain("http://localhost/Account/Login");

#pragma warning disable 4014
        FakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014


        FakeRequiredService.Claims.AddRange(new[]
        {
                new Claim(TiknasClaimTypes.UserId, Guid.NewGuid().ToString())
            });

        result = await GetResponseAsync("/api/exception-test/TiknasAuthorizationException", HttpStatusCode.Redirect);
        result.Headers.Location.ToString().ShouldContain("http://localhost/Account/AccessDenied");

#pragma warning disable 4014
        FakeExceptionSubscriber
            .Received()
            .HandleAsync(Arg.Any<ExceptionNotificationContext>());
#pragma warning restore 4014
    }
}
