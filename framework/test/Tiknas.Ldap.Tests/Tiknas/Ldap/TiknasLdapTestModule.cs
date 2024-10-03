using Tiknas.Autofac;
using Tiknas.Modularity;
using Tiknas.Settings;

namespace Tiknas.Ldap;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasLdapModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasLdapTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSettingOptions>(options =>
        {
            options.ValueProviders.Add<TestLdapSettingValueProvider>();
        });
    }
}
