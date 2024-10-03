using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.ExceptionHandling;

[DependsOn(
    typeof(TiknasExceptionHandlingModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasExceptionHandlingTestModule : TiknasModule
{

}
