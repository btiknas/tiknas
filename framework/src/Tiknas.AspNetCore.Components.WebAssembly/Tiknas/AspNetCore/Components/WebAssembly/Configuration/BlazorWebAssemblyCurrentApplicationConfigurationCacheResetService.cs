using System.Threading.Tasks;
using Tiknas.AspNetCore.Components.Web.Configuration;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.WebAssembly.Configuration;

[Dependency(ReplaceServices = true)]
public class BlazorWebAssemblyCurrentApplicationConfigurationCacheResetService :
    ICurrentApplicationConfigurationCacheResetService,
    ITransientDependency
{
    private readonly WebAssemblyCachedApplicationConfigurationClient _webAssemblyCachedApplicationConfigurationClient;

    public BlazorWebAssemblyCurrentApplicationConfigurationCacheResetService(WebAssemblyCachedApplicationConfigurationClient webAssemblyCachedApplicationConfigurationClient)
    {
        _webAssemblyCachedApplicationConfigurationClient = webAssemblyCachedApplicationConfigurationClient;
    }

    public async Task ResetAsync()
    {
        await _webAssemblyCachedApplicationConfigurationClient.InitializeAsync();
    }
}
