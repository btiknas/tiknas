using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Tiknas.Features;

public interface IFeatureChecker
{
    Task<string?> GetOrNullAsync([NotNull] string name);

    Task<bool> IsEnabledAsync(string name);
}
