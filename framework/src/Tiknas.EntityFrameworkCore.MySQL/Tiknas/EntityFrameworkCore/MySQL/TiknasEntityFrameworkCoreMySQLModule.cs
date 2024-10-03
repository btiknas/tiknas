using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.Guids;
using Tiknas.Modularity;

namespace Tiknas.EntityFrameworkCore.MySQL;

[DependsOn(
    typeof(TiknasEntityFrameworkCoreModule)
    )]
public class TiknasEntityFrameworkCoreMySQLModule : TiknasModule
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
