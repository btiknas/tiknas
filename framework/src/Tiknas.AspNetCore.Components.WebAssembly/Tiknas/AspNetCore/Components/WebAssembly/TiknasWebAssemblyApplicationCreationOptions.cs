using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Tiknas.AspNetCore.Components.WebAssembly;

public class TiknasWebAssemblyApplicationCreationOptions
{
    public WebAssemblyHostBuilder HostBuilder { get; }

    public TiknasApplicationCreationOptions ApplicationCreationOptions { get; }

    public TiknasWebAssemblyApplicationCreationOptions(
        WebAssemblyHostBuilder hostBuilder,
        TiknasApplicationCreationOptions applicationCreationOptions)
    {
        HostBuilder = hostBuilder;
        ApplicationCreationOptions = applicationCreationOptions;
    }
}
