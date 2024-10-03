using Microsoft.Extensions.DependencyInjection;
using Tiknas.EntityFrameworkCore.GlobalFilters;
using Tiknas.Guids;
using Tiknas.Modularity;

namespace Tiknas.EntityFrameworkCore.Oracle;

[DependsOn(typeof(TiknasEntityFrameworkCoreModule))]
public class TiknasEntityFrameworkCoreOracleModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSequentialGuidGeneratorOptions>(options =>
        {
            if (options.DefaultSequentialGuidType == null)
            {
                options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsBinary;
            }
        });

        Configure<TiknasEfCoreGlobalFilterOptions>(options =>
        {
            options.UseDbFunction = true;
        });
    }
}
