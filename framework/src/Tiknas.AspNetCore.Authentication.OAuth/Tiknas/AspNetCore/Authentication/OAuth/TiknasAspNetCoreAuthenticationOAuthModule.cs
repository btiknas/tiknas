using Tiknas.Modularity;
using Tiknas.Security;

namespace Tiknas.AspNetCore.Authentication.OAuth;

[DependsOn(typeof(TiknasSecurityModule))]
public class TiknasAspNetCoreAuthenticationOAuthModule : TiknasModule
{

}
