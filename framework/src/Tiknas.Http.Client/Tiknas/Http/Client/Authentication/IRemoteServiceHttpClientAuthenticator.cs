using System.Threading.Tasks;

namespace Tiknas.Http.Client.Authentication;

public interface IRemoteServiceHttpClientAuthenticator
{
    Task Authenticate(RemoteServiceHttpClientAuthenticateContext context); //TODO: Rename to AuthenticateAsync
}
