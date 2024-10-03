using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Imaging;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasImagingAspNetCoreModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasImagingAspNetCoreTestModule : TiknasModule
{

}