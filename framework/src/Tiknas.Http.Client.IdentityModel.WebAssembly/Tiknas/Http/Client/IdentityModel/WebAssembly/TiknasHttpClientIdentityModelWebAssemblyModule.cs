using IdentityModel;
using Tiknas.AspNetCore.Components.WebAssembly;
using Tiknas.Modularity;
using Tiknas.Security.Claims;

namespace Tiknas.Http.Client.IdentityModel.WebAssembly;

[DependsOn(
    typeof(TiknasHttpClientIdentityModelModule),
    typeof(TiknasAspNetCoreComponentsWebAssemblyModule)
)]
public class TiknasHttpClientIdentityModelWebAssemblyModule : TiknasModule
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
