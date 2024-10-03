using Tiknas.Ldap.Localization;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.Settings;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Ldap;

[DependsOn(
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasLocalizationModule))]
public class TiknasLdapAbstractionsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasLdapAbstractionsModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LdapResource>("en")
                .AddVirtualJson("/Tiknas/Ldap/Localization");
        });
    }
}
