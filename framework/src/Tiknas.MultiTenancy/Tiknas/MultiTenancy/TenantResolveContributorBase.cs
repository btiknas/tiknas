using System.Threading.Tasks;

namespace Tiknas.MultiTenancy;

public abstract class TenantResolveContributorBase : ITenantResolveContributor
{
    public abstract string Name { get; }

    public abstract Task ResolveAsync(ITenantResolveContext context);
}
