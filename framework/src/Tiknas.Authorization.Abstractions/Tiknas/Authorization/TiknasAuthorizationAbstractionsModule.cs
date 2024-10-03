using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.Authorization;

[DependsOn(
    typeof(TiknasMultiTenancyAbstractionsModule)
)]
public class TiknasAuthorizationAbstractionsModule : TiknasModule
{

}
