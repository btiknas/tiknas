using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

public interface IBundlerContext
{
    string BundleRelativePath { get; }

    IReadOnlyList<string> ContentFiles { get; }

    bool IsMinificationEnabled { get; }
}
