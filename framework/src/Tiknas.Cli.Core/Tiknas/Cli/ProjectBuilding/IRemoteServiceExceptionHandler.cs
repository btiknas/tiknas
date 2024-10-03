using System.Net.Http;
using System.Threading.Tasks;

namespace Tiknas.Cli.ProjectBuilding;

public interface IRemoteServiceExceptionHandler
{
    Task EnsureSuccessfulHttpResponseAsync(HttpResponseMessage responseMessage);

    Task<string> GetTiknasRemoteServiceErrorAsync(HttpResponseMessage responseMessage);
}
