using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Imaging;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasImagingSkiaSharpModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasImagingSkiaSharpTestModule : TiknasModule
{

}
