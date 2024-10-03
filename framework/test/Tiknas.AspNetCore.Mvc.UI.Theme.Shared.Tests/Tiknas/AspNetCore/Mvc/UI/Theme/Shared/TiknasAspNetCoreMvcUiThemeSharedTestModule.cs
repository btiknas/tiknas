using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Tests.Tiknas.AspNetCore.Mvc.UI.Theme.Shared;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAspNetCoreMvcUiThemeSharedModule)
)]
public class TiknasAspNetCoreMvcUiThemeSharedTestModule : TiknasModule
{

}
