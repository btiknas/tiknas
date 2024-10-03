using Tiknas.Modularity;
using Tiknas.Validation;
using Tiknas.Localization;

namespace Tiknas.ObjectExtending;

[DependsOn(
    typeof(TiknasLocalizationAbstractionsModule),
    typeof(TiknasValidationAbstractionsModule)
    )]
public class TiknasObjectExtendingModule : TiknasModule
{

}
