using System.Collections.Generic;

namespace Tiknas.Features;

public interface IFeatureValueProviderManager
{
    IReadOnlyList<IFeatureValueProvider> ValueProviders { get; }
}