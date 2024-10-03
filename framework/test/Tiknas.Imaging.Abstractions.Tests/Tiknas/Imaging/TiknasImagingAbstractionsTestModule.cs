using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.Imaging;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasImagingAbstractionsModule),
    typeof(TiknasTestBaseModule)
)]
public class TiknasImagingAbstractionsTestModule : TiknasModule
{

}