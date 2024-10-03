using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.MultiTenancy;

[DependsOn(
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasAspNetCoreModule)
    )]
public class TiknasAspNetCoreMultiTenancyModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasTenantResolveOptions>(options =>
        {
            options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
            options.TenantResolvers.Add(new RouteTenantResolveContributor());
            options.TenantResolvers.Add(new HeaderTenantResolveContributor());
            options.TenantResolvers.Add(new CookieTenantResolveContributor());
        });
    }
}
