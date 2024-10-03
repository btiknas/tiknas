using Tiknas.Modularity;
using Tiknas.Settings;

namespace Tiknas.Ldap;

[DependsOn(
    typeof(TiknasLdapAbstractionsModule),
    typeof(TiknasSettingsModule))]
public class TiknasLdapModule : TiknasModule
{
   
}
