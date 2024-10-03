using Tiknas.Autofac;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.Sqlite;
using Tiknas.Modularity;

namespace Tiknas.Dapper;

[DependsOn(
    typeof(TiknasEntityFrameworkCoreTestModule),
    typeof(TiknasDapperModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasDapperTestModule : TiknasModule
{

}
