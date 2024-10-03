using Tiknas.IdentityModel;
using Tiknas.Modularity;

namespace Tiknas.Http.Client.IdentityModel;

[DependsOn(
    typeof(TiknasHttpClientModule),
    typeof(TiknasIdentityModelModule)
    )]
public class TiknasHttpClientIdentityModelModule : TiknasModule
{

}
