using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web;

public interface IServerUrlProvider
{
    Task<string> GetBaseUrlAsync(string? remoteServiceName = null);
}
