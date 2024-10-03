using Tiknas.DependencyInjection;

namespace Tiknas.MultiTenancy;

public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonDependency
{
    public TenantResolveResult? Result {
        get => null;
        set { }
    }
}
