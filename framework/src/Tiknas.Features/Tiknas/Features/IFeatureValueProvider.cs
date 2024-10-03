using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Features;

public interface IFeatureValueProvider
{
    string Name { get; }

    Task<string?> GetOrNullAsync([NotNull] FeatureDefinition feature);
}
