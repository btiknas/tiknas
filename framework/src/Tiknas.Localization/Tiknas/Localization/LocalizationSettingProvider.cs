using Tiknas.Localization.Resources.TiknasLocalization;
using Tiknas.Settings;

namespace Tiknas.Localization;

public class LocalizationSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                "en",
                L("DisplayName:Tiknas.Localization.DefaultLanguage"),
                L("Description:Tiknas.Localization.DefaultLanguage"),
                isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TiknasLocalizationResource>(name);
    }
}
