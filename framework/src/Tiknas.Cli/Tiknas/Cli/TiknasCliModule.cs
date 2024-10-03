using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Cli;

[DependsOn(
    typeof(TiknasCliCoreModule),
    typeof(TiknasAutofacModule)
)]
public class TiknasCliModule : TiknasModule
{

}
