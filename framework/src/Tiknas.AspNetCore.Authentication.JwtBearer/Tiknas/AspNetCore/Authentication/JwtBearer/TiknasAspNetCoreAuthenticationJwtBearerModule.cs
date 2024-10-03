using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Authentication.JwtBearer.DynamicClaims;
using Tiknas.Caching;
using Tiknas.Modularity;
using Tiknas.Security;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Authentication.JwtBearer;

[DependsOn(typeof(TiknasSecurityModule), typeof(TiknasCachingModule), typeof(TiknasAspNetCoreAbstractionsModule))]
public class TiknasAspNetCoreAuthenticationJwtBearerModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services.AddHttpContextAccessor();

        if (context.Services.ExecutePreConfiguredActions<WebRemoteDynamicClaimsPrincipalContributorOptions>().IsEnabled &&
            context.Services.ExecutePreConfiguredActions<TiknasClaimsPrincipalFactoryOptions>().IsRemoteRefreshEnabled)
        {
            context.Services.AddTransient<WebRemoteDynamicClaimsPrincipalContributor>();
            context.Services.AddTransient<WebRemoteDynamicClaimsPrincipalContributorCache>();
        }

    }
}
