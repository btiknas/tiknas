using Tiknas.Modularity;
using Tiknas.ObjectExtending.TestObjects;
using Tiknas.Threading;

namespace Tiknas.ObjectExtending;

[DependsOn(
    typeof(TiknasObjectExtendingModule),
    typeof(TiknasTestBaseModule)
    )]
public class TiknasObjectExtendingTestModule : TiknasModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPerson, int>("Age")
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("NoPairCheck", options => options.CheckPairDefinitionOnMapping = false)
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("CityName")
                .AddOrUpdateProperty<ExtensibleTestPerson, ExtensibleTestEnumProperty>("EnumProperty")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, int>("ChildCount")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, string>("CityName")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, ExtensibleTestEnumProperty>("EnumProperty")
                .AddOrUpdateProperty<ExtensibleTestPersonWithRegularPropertiesDto, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPersonWithRegularPropertiesDto, int>("Age");
        });
    }
}
