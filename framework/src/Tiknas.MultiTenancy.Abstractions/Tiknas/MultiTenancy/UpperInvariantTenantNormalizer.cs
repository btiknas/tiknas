using Tiknas.DependencyInjection;

namespace Tiknas.MultiTenancy;

public class UpperInvariantTenantNormalizer : ITenantNormalizer, ITransientDependency
{
    public virtual string? NormalizeName(string? name)
    {
        return name?.Normalize().ToUpperInvariant();
    }
}
