using Tiknas.Application;
using Tiknas.Autofac;
using Tiknas.ExceptionHandling;
using Tiknas.Modularity;

namespace Tiknas.GlobalFeatures;

[DependsOn(typeof(TiknasAutofacModule))]
[DependsOn(typeof(TiknasGlobalFeaturesModule))]
[DependsOn(typeof(TiknasDddApplicationModule))]
[DependsOn(typeof(TiknasExceptionHandlingModule))]
public class GlobalFeatureTestModule : TiknasModule
{

}
