using Shouldly;
using Tiknas.DynamicProxy;
using Tiknas.Http.Client.Authentication;
using Tiknas.Http.Client.IdentityModel.Web.Tests;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Http.Client.IdentityModel.Web;

public class HttpContextIdentityModelRemoteServiceHttpClientAuthenticator_Tests : TiknasIntegratedTest<TiknasHttpClientIdentityModelWebTestModule>
{
    private readonly IRemoteServiceHttpClientAuthenticator _remoteServiceHttpClientAuthenticator;

    public HttpContextIdentityModelRemoteServiceHttpClientAuthenticator_Tests()
    {
        _remoteServiceHttpClientAuthenticator = GetRequiredService<IRemoteServiceHttpClientAuthenticator>();
    }

    [Fact]
    public void Implementation_Should_Be_Type_Of_HttpContextIdentityModelRemoteServiceHttpClientAuthenticator()
    {
        ProxyHelper.UnProxy(_remoteServiceHttpClientAuthenticator)
            .ShouldBeOfType(typeof(HttpContextIdentityModelRemoteServiceHttpClientAuthenticator));
    }
}
