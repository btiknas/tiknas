using Tiknas.Autofac;
using Tiknas.Json.Newtonsoft;
using Tiknas.Json.SystemTextJson;
using Tiknas.Modularity;

namespace Tiknas.Json;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasJsonSystemTextJsonModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasJsonSystemTextJsonTestModule : TiknasModule
{

}

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasJsonNewtonsoftModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasJsonNewtonsoftTestModule : TiknasModule
{

}
