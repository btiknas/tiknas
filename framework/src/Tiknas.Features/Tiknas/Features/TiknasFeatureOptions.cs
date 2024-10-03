using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.Features;

public class TiknasFeatureOptions
{
    public ITypeList<IFeatureDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IFeatureValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedFeatures { get; }

    public HashSet<string> DeletedFeatureGroups { get; }

    public TiknasFeatureOptions()
    {
        DefinitionProviders = new TypeList<IFeatureDefinitionProvider>();
        ValueProviders = new TypeList<IFeatureValueProvider>();

        DeletedFeatures = new HashSet<string>();
        DeletedFeatureGroups = new HashSet<string>();
    }
}
