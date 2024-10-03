using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client.Authentication;
using Tiknas.IdentityModel;

namespace Tiknas.Http.Client.IdentityModel;

[Dependency(ReplaceServices = true)]
public class IdentityModelRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ITransientDependency
{
    protected IIdentityModelAuthenticationService IdentityModelAuthenticationService { get; }

    public IdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService)
    {
        IdentityModelAuthenticationService = identityModelAuthenticationService;
    }

    public virtual async Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        await IdentityModelAuthenticationService.TryAuthenticateAsync(
            context.Client,
            context.RemoteService.GetIdentityClient() ?? context.RemoteServiceName
        );
    }
}
