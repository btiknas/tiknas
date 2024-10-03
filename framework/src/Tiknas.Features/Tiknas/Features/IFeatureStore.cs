using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tiknas.Features;

public interface IFeatureStore
{
    Task<string?> GetOrNullAsync(
        [NotNull] string name,
        string? providerName,
        string? providerKey
    );
}
