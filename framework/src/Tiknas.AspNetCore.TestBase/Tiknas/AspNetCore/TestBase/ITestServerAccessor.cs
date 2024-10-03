using Microsoft.AspNetCore.TestHost;

namespace Tiknas.AspNetCore.TestBase;

public interface ITestServerAccessor
{
    TestServer Server { get; set; }
}
