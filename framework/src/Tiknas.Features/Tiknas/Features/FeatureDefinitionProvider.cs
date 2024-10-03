using Tiknas.DependencyInjection;

namespace Tiknas.Features;

public abstract class FeatureDefinitionProvider : IFeatureDefinitionProvider, ITransientDependency
{
    public abstract void Define(IFeatureDefinitionContext context);
}
