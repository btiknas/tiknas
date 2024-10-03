using JetBrains.Annotations;
using Tiknas.DependencyInjection;

namespace Tiknas.MultiTenancy;

public interface ITenantResolveContext : IServiceProviderAccessor
{
    string? TenantIdOrName { get; set; }

    bool Handled { get; set; }
}
