using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.Configuration;

public class NullCurrentApplicationConfigurationCacheResetService : ICurrentApplicationConfigurationCacheResetService, ISingletonDependency
{
    public Task ResetAsync()
    {
        return Task.CompletedTask;
    }
}
