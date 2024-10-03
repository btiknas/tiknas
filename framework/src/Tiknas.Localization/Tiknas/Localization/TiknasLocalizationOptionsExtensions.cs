using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Tiknas.Localization;

public static class TiknasLocalizationOptionsExtensions
{
    public static TiknasLocalizationOptions AddLanguagesMapOrUpdate(this TiknasLocalizationOptions localizationOptions,
        string packageName, params NameValue[] maps)
    {
        foreach (var map in maps)
        {
            AddOrUpdate(localizationOptions.LanguagesMap, packageName, map);
        }

        return localizationOptions;
    }

    public static string GetLanguagesMap(this TiknasLocalizationOptions localizationOptions, string packageName,
        string language)
    {
        return localizationOptions.LanguagesMap.TryGetValue(packageName, out var maps)
            ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
            : language;
    }

    public static string GetCurrentUICultureLanguagesMap(this TiknasLocalizationOptions localizationOptions, string packageName)
    {
        return GetLanguagesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
    }

    public static TiknasLocalizationOptions AddLanguageFilesMapOrUpdate(this TiknasLocalizationOptions localizationOptions,
        string packageName, params NameValue[] maps)
    {
        foreach (var map in maps)
        {
            AddOrUpdate(localizationOptions.LanguageFilesMap, packageName, map);
        }

        return localizationOptions;
    }

    public static string GetLanguageFilesMap(this TiknasLocalizationOptions localizationOptions, string packageName,
        string language)
    {
        return localizationOptions.LanguageFilesMap.TryGetValue(packageName, out var maps)
            ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
            : language;
    }

    public static string GetCurrentUICultureLanguageFilesMap(this TiknasLocalizationOptions localizationOptions, string packageName)
    {
        return GetLanguageFilesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
    }

    private static void AddOrUpdate(IDictionary<string, List<NameValue>> maps, string packageName, NameValue value)
    {
        if (maps.TryGetValue(packageName, out var existMaps))
        {
            existMaps.GetOrAdd(x => x.Name == value.Name, () => value).Value = value.Value;
        }
        else
        {
            maps.Add(packageName, new List<NameValue> { value });
        }
    }
}
