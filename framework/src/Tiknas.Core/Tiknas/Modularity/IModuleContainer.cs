using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.Modularity;

public interface IModuleContainer
{
    [NotNull]
    IReadOnlyList<ITiknasModuleDescriptor> Modules { get; }
}
