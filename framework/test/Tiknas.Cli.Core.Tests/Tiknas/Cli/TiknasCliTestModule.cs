using Tiknas.Modularity;

namespace Tiknas.Cli;

[DependsOn(
    typeof(TiknasTestBaseModule),
    typeof(TiknasCliCoreModule)
    )]
public class TiknasCliTestModule : TiknasModule
{

}
