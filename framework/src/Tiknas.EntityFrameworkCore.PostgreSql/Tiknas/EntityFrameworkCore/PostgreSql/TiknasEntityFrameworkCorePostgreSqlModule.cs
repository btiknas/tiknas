using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.Guids;
using Tiknas.Modularity;

namespace Tiknas.EntityFrameworkCore.PostgreSql;

[DependsOn(
    typeof(TiknasEntityFrameworkCoreModule)
    )]
public class TiknasEntityFrameworkCorePostgreSqlModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
            }
        });

        Configure<TiknasEfCoreGlobalFilterOptions>(options =>
        {
            options.UseDbFunction = true;
        });
    }
}
