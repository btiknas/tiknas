using System;
using JetBrains.Annotations;

namespace Tiknas.Modularity;

public interface IDependedTypesProvider
{
    [NotNull]
    Type[] GetDependedTypes();
}
