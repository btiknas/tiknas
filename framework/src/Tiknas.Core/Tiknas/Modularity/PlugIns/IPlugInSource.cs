using System;
using JetBrains.Annotations;

namespace Tiknas.Modularity.PlugIns;

public interface IPlugInSource
{
    [NotNull]
    Type[] GetModules();
}
