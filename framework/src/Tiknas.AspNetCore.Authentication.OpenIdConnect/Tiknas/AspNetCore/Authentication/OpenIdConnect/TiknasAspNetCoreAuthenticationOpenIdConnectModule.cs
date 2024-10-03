using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Authentication.OAuth;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.RemoteServices;

namespace Tiknas.AspNetCore.Authentication.OpenIdConnect;

[DependsOn(
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasAspNetCoreAuthenticationOAuthModule),
    typeof(TiknasRemoteServicesModule)
    )]
public class TiknasAspNetCoreAuthenticationOpenIdConnectModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
    }
}
