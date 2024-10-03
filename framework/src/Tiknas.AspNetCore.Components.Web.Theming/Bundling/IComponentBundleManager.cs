using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Theming.Bundling;

public interface IComponentBundleManager
{
    Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName);

    Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName);
}
