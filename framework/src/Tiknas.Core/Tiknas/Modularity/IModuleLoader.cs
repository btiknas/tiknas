using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity.PlugIns;

namespace Tiknas.Modularity;

public interface IModuleLoader
{
    [NotNull]
    ITiknasModuleDescriptor[] LoadModules(
        [NotNull] IServiceCollection services,
        [NotNull] Type startupModuleType,
        [NotNull] PlugInSourceList plugInSources
    );
}
