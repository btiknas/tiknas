using JetBrains.Annotations;

namespace Tiknas.MultiTenancy;

public interface ITenantResolveResultAccessor
{
    TenantResolveResult? Result { get; set; }
}
