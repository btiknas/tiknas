using Tiknas.Autofac;
using Tiknas.Http.Client;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.TestBase;

[DependsOn(typeof(TiknasHttpClientModule))]
[DependsOn(typeof(TiknasAspNetCoreModule))]
[DependsOn(typeof(TiknasTestBaseModule))]
[DependsOn(typeof(TiknasAutofacModule))]
public class TiknasAspNetCoreTestBaseModule : TiknasModule
{

}
