using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.Modularity;

namespace Tiknas.EntityFrameworkCore.Sqlite;

[DependsOn(
    typeof(TiknasEntityFrameworkCoreModule)
)]
public class TiknasEntityFrameworkCoreSqliteModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasEfCoreGlobalFilterOptions>(options =>
        {
            options.UseDbFunction = true;
        });
    }
}
