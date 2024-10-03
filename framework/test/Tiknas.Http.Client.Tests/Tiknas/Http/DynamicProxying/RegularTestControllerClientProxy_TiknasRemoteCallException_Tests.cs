using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Http.Client;
using Tiknas.Http.Client.Proxying;
using Xunit;

namespace Tiknas.Http.DynamicProxying;

public class RegularTestControllerClientProxy_TiknasRemoteCallException_Tests : TiknasHttpClientTestBase
{
    private readonly IRegularTestController _controller;

    public RegularTestControllerClientProxy_TiknasRemoteCallException_Tests()
    {
        _controller = ServiceProvider.GetRequiredService<IRegularTestController>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IProxyHttpClientFactory, TestProxyHttpClientFactory>());
    }

    [Fact]
    public async Task TiknasRemoteCallException_On_SendAsync_Test()
    {
        var exception = await Assert.ThrowsAsync<TiknasRemoteCallException>(async () => await _controller.AbortRequestAsync(default));
        exception.Message.ShouldContain("An error occurred during the TIKNAS remote HTTP request.");
    }

    class TestProxyHttpClientFactory : IProxyHttpClientFactory
    {
        private readonly ITestServerAccessor _testServerAccessor;

        private int _count;

        public TestProxyHttpClientFactory(ITestServerAccessor testServerAccessor)
        {
            _testServerAccessor = testServerAccessor;
        }

        public HttpClient Create(string name) => Create();

        public HttpClient Create()
        {
            if (_count++ > 0)
            {
                //Will get an error on the SendAsync method.
                return new HttpClient();
            }

            // for DynamicHttpProxyInterceptor.GetActionApiDescriptionModel
            return _testServerAccessor.Server.CreateClient();
        }
    }
}
