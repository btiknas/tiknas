using Tiknas.Autofac;
using Tiknas.ExceptionHandling;
using Tiknas.Modularity;

namespace Tiknas.Features;

[DependsOn(
    typeof(TiknasFeaturesModule),
    typeof(TiknasExceptionHandlingModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasFeaturesTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}
