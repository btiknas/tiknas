using System.Net.Http;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client;
using Tiknas.Http.Client.DynamicProxying;
using Tiknas.Http.Client.Proxying;

namespace Tiknas.AspNetCore.TestBase.DynamicProxying;

[Dependency(ReplaceServices = true)]
public class AspNetCoreTestProxyHttpClientFactory : IProxyHttpClientFactory, ITransientDependency
{
    private readonly ITestServerAccessor _testServerAccessor;

    public AspNetCoreTestProxyHttpClientFactory(
        ITestServerAccessor testServerAccessor)
    {
        _testServerAccessor = testServerAccessor;
    }

    public HttpClient Create()
    {
        return _testServerAccessor.Server.CreateClient();
    }

    public HttpClient Create(string name)
    {
        return Create();
    }
}
