using Tiknas.Ldap.Localization;
using Tiknas.Localization;
using Tiknas.Settings;

namespace Tiknas.Ldap;

public class LdapSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                LdapSettingNames.Ldaps,
                "false",
                L("DisplayName:Tiknas.Ldap.Ldaps"),
                L("Description:Tiknas.Ldap.Ldaps")),

            new SettingDefinition(
                LdapSettingNames.ServerHost,
                "",
                L("DisplayName:Tiknas.Ldap.ServerHost"),
                L("Description:Tiknas.Ldap.ServerHost")),

            new SettingDefinition(
                LdapSettingNames.ServerPort,
                "389",
                L("DisplayName:Tiknas.Ldap.ServerPort"),
                L("Description:Tiknas.Ldap.ServerPort")),

            new SettingDefinition(
                LdapSettingNames.BaseDc,
                "",
                L("DisplayName:Tiknas.Ldap.BaseDc"),
                L("Description:Tiknas.Ldap.BaseDc")),

            new SettingDefinition(
                LdapSettingNames.Domain,
                "",
                L("DisplayName:Tiknas.Ldap.Domain"),
                L("Description:Tiknas.Ldap.Domain")),

            new SettingDefinition(
                LdapSettingNames.UserName,
                "",
                L("DisplayName:Tiknas.Ldap.UserName"),
                L("Description:Tiknas.Ldap.UserName")),

            new SettingDefinition(
                LdapSettingNames.Password,
                "",
                L("DisplayName:Tiknas.Ldap.Password"),
                L("Description:Tiknas.Ldap.Password"),
                isEncrypted: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LdapResource>(name);
    }
}
