using System;
using Microsoft.Extensions.Localization;

namespace Tiknas.Localization;

public interface ITiknasEnumLocalizer
{
    string GetString(Type enumType, object enumValue);

    string GetString(Type enumType, object enumValue, IStringLocalizer?[] specifyLocalizers);
}
