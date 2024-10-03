using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Imaging;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasImagingMagickNetModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasImagingMagickNetTestModule : TiknasModule
{

}