using Tiknas.Application;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc;

[DependsOn(
    typeof(TiknasDddApplicationContractsModule)
    )]
public class TiknasAspNetCoreMvcContractsModule : TiknasModule
{

}
