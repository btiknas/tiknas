using Tiknas.Modularity;

namespace Tiknas.ObjectMapping;

[DependsOn(
    typeof(TiknasObjectMappingModule),
    typeof(TiknasTestBaseModule)
    )]
public class TiknasObjectMappingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTest1AutoObjectMappingProvider<MappingContext1>();
        context.Services.AddTest2AutoObjectMappingProvider<MappingContext2>();
    }
}
