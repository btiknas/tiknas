using IdentityModel;
using Tiknas.AspNetCore.Components.MauiBlazor;
using Tiknas.Modularity;
using Tiknas.Security.Claims;

namespace Tiknas.Http.Client.IdentityModel.MauiBlazor;

[DependsOn(
    typeof(TiknasHttpClientIdentityModelModule),
    typeof(TiknasAspNetCoreComponentsMauiBlazorModule)
)]
public class TiknasHttpClientIdentityModelMauiBlazorModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        TiknasClaimTypes.UserName = JwtClaimTypes.PreferredUserName;
        TiknasClaimTypes.Name = JwtClaimTypes.GivenName;
        TiknasClaimTypes.SurName = JwtClaimTypes.FamilyName;
        TiknasClaimTypes.UserId = JwtClaimTypes.Subject;
        TiknasClaimTypes.Role = JwtClaimTypes.Role;
        TiknasClaimTypes.Email = JwtClaimTypes.Email;
    }
}
