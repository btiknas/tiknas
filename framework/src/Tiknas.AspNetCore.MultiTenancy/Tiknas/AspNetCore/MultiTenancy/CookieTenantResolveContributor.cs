using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.MultiTenancy;

public class CookieTenantResolveContributor : HttpTenantResolveContributorBase
{
    public const string ContributorName = "Cookie";

    public override string Name => ContributorName;

    protected override Task<string?> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
    {
        return Task.FromResult(httpContext.Request.Cookies[context.GetTiknasAspNetCoreMultiTenancyOptions().TenantKey]);
    }
}
