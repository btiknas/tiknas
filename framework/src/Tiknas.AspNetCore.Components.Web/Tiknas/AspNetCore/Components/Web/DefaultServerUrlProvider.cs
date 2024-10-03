using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web;

public class DefaultServerUrlProvider : IServerUrlProvider, ISingletonDependency
{
    public Task<string> GetBaseUrlAsync(string? remoteServiceName = null)
    {
        return Task.FromResult("/");
    }
}
