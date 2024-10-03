using System;
using JetBrains.Annotations;

namespace Tiknas.Localization;

public interface IInheritedResourceTypesProvider
{
    [NotNull]
    Type[] GetInheritedResourceTypes();
}
