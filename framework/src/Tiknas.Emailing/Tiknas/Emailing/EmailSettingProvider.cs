using Tiknas.Emailing.Localization;
using Tiknas.Localization;
using Tiknas.Settings;

namespace Tiknas.Emailing;

/// <summary>
/// Defines settings to send emails.
/// <see cref="EmailSettingNames"/> for all available configurations.
/// </summary>
internal class EmailSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                EmailSettingNames.Smtp.Host,
                "127.0.0.1",
                L("DisplayName:Tiknas.Mailing.Smtp.Host"),
                L("Description:Tiknas.Mailing.Smtp.Host")),

            new SettingDefinition(EmailSettingNames.Smtp.Port,
                "25",
                L("DisplayName:Tiknas.Mailing.Smtp.Port"),
                L("Description:Tiknas.Mailing.Smtp.Port")),

            new SettingDefinition(
                EmailSettingNames.Smtp.UserName,
                displayName: L("DisplayName:Tiknas.Mailing.Smtp.UserName"),
                description: L("Description:Tiknas.Mailing.Smtp.UserName")),

            new SettingDefinition(
                EmailSettingNames.Smtp.Password,
                displayName:
                L("DisplayName:Tiknas.Mailing.Smtp.Password"),
                description: L("Description:Tiknas.Mailing.Smtp.Password"),
                isEncrypted: true),

            new SettingDefinition(
                EmailSettingNames.Smtp.Domain,
                displayName: L("DisplayName:Tiknas.Mailing.Smtp.Domain"),
                description: L("Description:Tiknas.Mailing.Smtp.Domain")),

            new SettingDefinition(
                EmailSettingNames.Smtp.EnableSsl,
                "false",
                L("DisplayName:Tiknas.Mailing.Smtp.EnableSsl"),
                L("Description:Tiknas.Mailing.Smtp.EnableSsl")),

            new SettingDefinition(
                EmailSettingNames.Smtp.UseDefaultCredentials,
                "true",
                L("DisplayName:Tiknas.Mailing.Smtp.UseDefaultCredentials"),
                L("Description:Tiknas.Mailing.Smtp.UseDefaultCredentials")),

            new SettingDefinition(
                EmailSettingNames.DefaultFromAddress,
                "noreply@tiknas.io",
                L("DisplayName:Tiknas.Mailing.DefaultFromAddress"),
                L("Description:Tiknas.Mailing.DefaultFromAddress")),

            new SettingDefinition(EmailSettingNames.DefaultFromDisplayName,
                "TIKNAS application",
                L("DisplayName:Tiknas.Mailing.DefaultFromDisplayName"),
                L("Description:Tiknas.Mailing.DefaultFromDisplayName"))
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmailingResource>(name);
    }
}
