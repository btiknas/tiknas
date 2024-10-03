using System.Collections.Generic;
using Tiknas.AspNetCore.MultiTenancy;

namespace Tiknas.MultiTenancy;

public static class TiknasMultiTenancyOptionsExtensions
{
    public static void AddDomainTenantResolver(this TiknasTenantResolveOptions options, string domainFormat)
    {
        options.TenantResolvers.InsertAfter(
            r => r is CurrentUserTenantResolveContributor,
            new DomainTenantResolveContributor(domainFormat)
        );
    }
}
