using System;
using Tiknas.Localization;

namespace Tiknas.ObjectExtending.Modularity;

public static class ExtensionPropertyConfigurationExtensions
{
    public static string? GetLocalizationResourceNameOrNull(
        this ExtensionPropertyConfiguration property)
    {
        if (property.DisplayName is LocalizableString localizableString)
        {
            return localizableString.ResourceName;
        }

        return null;
    }

    public static Type? GetLocalizationResourceTypeOrNull(
        this ExtensionPropertyConfiguration property)
    {
        if (property.DisplayName is LocalizableString localizableString)
        {
            return localizableString.ResourceType;
        }

        return null;
    }
}
