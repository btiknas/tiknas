using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.AspNetCore.Components.Web.Theming.Bundling;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.WebAssembly.Theming;

public class WebAssemblyComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    public virtual Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }

    public virtual Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return Task.FromResult<IReadOnlyList<string>>(new List<string>());
    }
}
