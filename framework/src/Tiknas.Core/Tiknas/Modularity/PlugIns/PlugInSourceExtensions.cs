using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Tiknas.Logging;

namespace Tiknas.Modularity.PlugIns;

public static class PlugInSourceExtensions
{
    [NotNull]
    public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource, ILogger logger)
    {
        Check.NotNull(plugInSource, nameof(plugInSource));

        return plugInSource
            .GetModules()
            .SelectMany(type => TiknasModuleHelper.FindAllModuleTypes(type, logger))
            .Distinct()
            .ToArray();
    }
}
