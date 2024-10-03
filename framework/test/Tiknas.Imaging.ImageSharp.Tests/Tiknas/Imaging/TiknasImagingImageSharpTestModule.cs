using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Imaging;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasImagingImageSharpModule),
    typeof(TiknasTestBaseModule)
)]

public class TiknasImagingImageSharpTestModule : TiknasModule
{

}