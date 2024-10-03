using System.Threading.Tasks;

namespace Tiknas.MultiTenancy;

public interface ITenantResolveContributor
{
    string Name { get; }

    Task ResolveAsync(ITenantResolveContext context);
}
