using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiknas.Features;

public interface IStaticFeatureDefinitionStore
{
    Task<FeatureDefinition?> GetOrNullAsync(string name);

    Task<IReadOnlyList<FeatureDefinition>> GetFeaturesAsync();

    Task<IReadOnlyList<FeatureGroupDefinition>> GetGroupsAsync();
}
