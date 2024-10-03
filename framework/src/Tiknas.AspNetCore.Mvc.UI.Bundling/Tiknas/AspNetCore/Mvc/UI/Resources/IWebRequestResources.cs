using System.Collections.Generic;
using Tiknas.AspNetCore.Mvc.UI.Bundling;

namespace Tiknas.AspNetCore.Mvc.UI.Resources;

public interface IWebRequestResources
{
    List<BundleFile> TryAdd(List<BundleFile> resources);
}
