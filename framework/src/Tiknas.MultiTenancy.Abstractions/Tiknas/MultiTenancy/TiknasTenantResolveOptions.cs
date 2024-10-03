using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.MultiTenancy;

public class TiknasTenantResolveOptions
{
    [NotNull]
    public List<ITenantResolveContributor> TenantResolvers { get; }

    public TiknasTenantResolveOptions()
    {
        TenantResolvers = new List<ITenantResolveContributor>();
    }
}
