using JetBrains.Annotations;
using Tiknas.Localization;

namespace Tiknas.Features;

public interface IFeatureDefinitionContext
{
    FeatureGroupDefinition AddGroup([NotNull] string name, ILocalizableString? displayName = null);

    FeatureGroupDefinition? GetGroupOrNull(string name);

    void RemoveGroup(string name);
}
