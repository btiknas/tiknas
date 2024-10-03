using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.UI;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAspNetCoreMvcUiBundlingModule),
    typeof(TiknasAutofacModule)
)]
public class TiknasAspNetCoreMvcUiTestModule : TiknasModule
{

}
