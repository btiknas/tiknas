using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiknas.Localization;

namespace Tiknas.BlazoriseUI.Components.ObjectExtending;

public static class EnumHelper
{
    [Obsolete("Use ITiknasEnumLocalizer instead.")]
    public static string GetLocalizedMemberName(Type enumType, object value, IStringLocalizerFactory stringLocalizerFactory)
    {
        var memberName = enumType.GetEnumName(value);
        var localizedMemberName = TiknasInternalLocalizationHelper.LocalizeWithFallback(
            new[]
            {
                stringLocalizerFactory.CreateDefaultOrNull()
            },
            new[]
            {
                $"Enum:{enumType.Name}.{value}",
                $"Enum:{enumType.Name}.{memberName}",
                $"{enumType.Name}.{value}",
                $"{enumType.Name}.{memberName}",
                memberName
            }!,
            memberName!
        );

        return localizedMemberName;
    }
}
