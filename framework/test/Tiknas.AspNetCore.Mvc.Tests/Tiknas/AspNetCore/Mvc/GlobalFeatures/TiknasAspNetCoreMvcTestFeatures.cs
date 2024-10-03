using JetBrains.Annotations;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

[GlobalFeatureName(Name)]
public class TiknasAspNetCoreMvcTestFeature1 : Tiknas.GlobalFeatures.GlobalFeature
{
    public const string Name = "TiknasAspNetCoreMvcTest.Feature1";

    internal TiknasAspNetCoreMvcTestFeature1([NotNull] TiknasAspNetCoreMvcTestFeatures tiknasAspNetCoreMvcTestFeatures)
        : base(tiknasAspNetCoreMvcTestFeatures)
    {

    }
}

public class TiknasAspNetCoreMvcTestFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "TiknasAspNetCoreMvcTest";

    public TiknasAspNetCoreMvcTestFeature1 Reactions => GetFeature<TiknasAspNetCoreMvcTestFeature1>();

    public TiknasAspNetCoreMvcTestFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new TiknasAspNetCoreMvcTestFeature1(this));
    }
}
