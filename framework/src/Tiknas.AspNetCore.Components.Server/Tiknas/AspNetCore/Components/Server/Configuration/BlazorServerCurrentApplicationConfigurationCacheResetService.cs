using System.Threading.Tasks;
using Tiknas.AspNetCore.Components.Web.Configuration;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.DependencyInjection;
using Tiknas.EventBus.Local;

namespace Tiknas.AspNetCore.Components.Server.Configuration;

[Dependency(ReplaceServices = true)]
public class BlazorServerCurrentApplicationConfigurationCacheResetService :
    ICurrentApplicationConfigurationCacheResetService,
    ITransientDependency
{
    private readonly ILocalEventBus _localEventBus;

    public BlazorServerCurrentApplicationConfigurationCacheResetService(
        ILocalEventBus localEventBus)
    {
        _localEventBus = localEventBus;
    }

    public async Task ResetAsync()
    {
        await _localEventBus.PublishAsync(
            new CurrentApplicationConfigurationCacheResetEventData()
        );
    }
}
