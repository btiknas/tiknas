using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Settings;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasSettingsModule),
    typeof(TiknasTestBaseModule)
    )]
public class TiknasSettingsTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSettingOptions>(options =>
        {
            options.ValueProviders.Add<TestSettingValueProvider>();
        });
    }
}
