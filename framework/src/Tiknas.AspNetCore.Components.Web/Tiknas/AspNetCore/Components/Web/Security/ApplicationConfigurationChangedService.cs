using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.Security;

public delegate void ApplicationConfigurationChangedHandler();

public class ApplicationConfigurationChangedService : IScopedDependency
{
    public event ApplicationConfigurationChangedHandler Changed = default!;

    public void NotifyChanged()
    {
        Changed?.Invoke();
    }
}
