using System;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.Features;

public abstract class FeatureCheckerBase : IFeatureChecker, ITransientDependency
{
    public abstract Task<string?> GetOrNullAsync(string name);

    public virtual async Task<bool> IsEnabledAsync(string name)
    {
        var value = await GetOrNullAsync(name);
        if (value.IsNullOrEmpty())
        {
            return false;
        }

        try
        {
            return bool.Parse(value!);
        }
        catch (Exception ex)
        {
            throw new TiknasException(
                $"The value '{value}' for the feature '{name}' should be a boolean, but was not!",
                ex
            );
        }
    }
}
