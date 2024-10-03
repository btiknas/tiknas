using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiknas.AspNetCore.Components.Web.Theming.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Server.Theming.Bundling;

public class BlazorServerComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    protected IBundleManager BundleManager { get; }

    public BlazorServerComponentBundleManager(IBundleManager bundleManager)
    {
        BundleManager = bundleManager;
    }

    public virtual async Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetStyleBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }

    public virtual async Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetScriptBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }
}
