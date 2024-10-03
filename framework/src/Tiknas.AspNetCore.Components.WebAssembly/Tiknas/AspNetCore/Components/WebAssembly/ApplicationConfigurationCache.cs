using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.WebAssembly;

public class ApplicationConfigurationCache : ISingletonDependency
{
    protected ApplicationConfigurationDto? Configuration { get; set; }

    public virtual ApplicationConfigurationDto? Get()
    {
        return Configuration;
    }

    public void Set(ApplicationConfigurationDto configuration)
    {
        Configuration = configuration;
    }
}
