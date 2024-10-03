using System;
using Microsoft.Extensions.Localization;

namespace Tiknas.Localization;

public static class TiknasEnumLocalizerExtensions
{
    public static string GetString<TEnum>(this ITiknasEnumLocalizer tiknasEnumLocalizer, object enumValue)
        where TEnum : Enum
    {
        return tiknasEnumLocalizer.GetString(typeof(TEnum), enumValue);
    }

    public static string GetString<TEnum>(this ITiknasEnumLocalizer tiknasEnumLocalizer, object enumValue, IStringLocalizer[] specifyLocalizers)
        where TEnum : Enum
    {
        return tiknasEnumLocalizer.GetString(typeof(TEnum), enumValue, specifyLocalizers);
    }
}
