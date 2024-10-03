using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.MailKit;

[DependsOn(
    typeof(TiknasMailKitModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasTestBaseModule))]
public class TiknasMailKitTestModule : TiknasModule
{
}
