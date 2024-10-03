using Tiknas.Autofac;
using Tiknas.Modularity;
using Tiknas.SecurityLog;

namespace Tiknas.Security;

[DependsOn(
    typeof(TiknasSecurityModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasSecurityTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSecurityLogOptions>(x =>
        {
            x.ApplicationName = "TiknasSecurityTest";
        });
    }
}
