using Tiknas.Localization;
using Tiknas.Settings;
using Tiknas.Timing.Localization.Resources.TiknasTiming;

namespace Tiknas.Timing;

public class TimingSettingProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(TimingSettingNames.TimeZone,
                "UTC",
                L("DisplayName:Tiknas.Timing.Timezone"),
                L("Description:Tiknas.Timing.Timezone"),
                isVisibleToClients: true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TiknasTimingResource>(name);
    }
}
