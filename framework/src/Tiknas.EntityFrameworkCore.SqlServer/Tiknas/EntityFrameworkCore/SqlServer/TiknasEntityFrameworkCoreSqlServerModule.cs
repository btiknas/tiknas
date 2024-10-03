using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.Guids;
using Tiknas.Modularity;

namespace Tiknas.EntityFrameworkCore.SqlServer;

[DependsOn(
    typeof(TiknasEntityFrameworkCoreModule)
    )]
public class TiknasEntityFrameworkCoreSqlServerModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
            }
        });

        Configure<TiknasEfCoreGlobalFilterOptions>(options =>
        {
            options.UseDbFunction = true;
        });
    }
}
