using Tiknas.Domain;
using Tiknas.EntityFrameworkCore;
using Tiknas.Modularity;

namespace Tiknas.Dapper;

[DependsOn(
    typeof(TiknasDddDomainModule),
    typeof(TiknasEntityFrameworkCoreModule))]
public class TiknasDapperModule : TiknasModule
{
}
