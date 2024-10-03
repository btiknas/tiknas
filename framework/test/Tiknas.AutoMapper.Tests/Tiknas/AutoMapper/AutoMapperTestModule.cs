using Tiknas.Modularity;
using Tiknas.ObjectExtending;

namespace Tiknas.AutoMapper;

[DependsOn(
    typeof(TiknasAutoMapperModule),
    typeof(TiknasObjectExtendingTestModule)
)]
public class AutoMapperTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasAutoMapperOptions>(options =>
        {
            options.AddMaps<AutoMapperTestModule>();
        });
    }
}
