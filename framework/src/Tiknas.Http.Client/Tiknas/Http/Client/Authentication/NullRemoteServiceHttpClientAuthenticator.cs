using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Http.Client.Authentication;

[Dependency(TryRegister = true)]
public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
{
    public Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        return Task.CompletedTask;
    }
}
