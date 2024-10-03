using Tiknas.Modularity;

namespace Tiknas.Minify;

[DependsOn(
    typeof(TiknasMinifyModule),
    typeof(TiknasTestBaseModule))]
public class TiknasMinifyTestModule : TiknasModule
{
}
