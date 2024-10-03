using Microsoft.AspNetCore.TestHost;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.TestBase;

public class TestServerAccessor : ITestServerAccessor, ISingletonDependency
{
    public TestServer Server { get; set; } = default!;
}
