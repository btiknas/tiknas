using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.MultiTenancy;

public static class TenantResolveContextExtensions
{
    public static TiknasAspNetCoreMultiTenancyOptions GetTiknasAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
    {
        return context.ServiceProvider.GetRequiredService<IOptions<TiknasAspNetCoreMultiTenancyOptions>>().Value;
    }
}
