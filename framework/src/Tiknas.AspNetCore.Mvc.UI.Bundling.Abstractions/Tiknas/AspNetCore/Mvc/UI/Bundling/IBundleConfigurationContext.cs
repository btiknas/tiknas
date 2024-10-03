using System.Collections.Generic;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

public interface IBundleConfigurationContext : IServiceProviderAccessor
{
    List<BundleFile> Files { get; }
}
