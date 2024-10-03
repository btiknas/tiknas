using System;
using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.Localization;

public class TiknasLocalizationOptions
{
    public LocalizationResourceDictionary Resources { get; }

    /// <summary>
    /// Used as the default resource when resource was not specified on a localization operation.
    /// </summary>
    public Type? DefaultResourceType { get; set; }

    public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

    public List<LanguageInfo> Languages { get; }

    public Dictionary<string, List<NameValue>> LanguagesMap { get; }

    public Dictionary<string, List<NameValue>> LanguageFilesMap { get; }

    public bool TryToGetFromBaseCulture { get; set; }

    public bool TryToGetFromDefaultCulture { get; set; }

    public TiknasLocalizationOptions()
    {
        Resources = new LocalizationResourceDictionary();
        GlobalContributors = new TypeList<ILocalizationResourceContributor>();
        Languages = new List<LanguageInfo>();
        LanguagesMap = new Dictionary<string, List<NameValue>>();
        LanguageFilesMap = new Dictionary<string, List<NameValue>>();
        TryToGetFromBaseCulture = true;
        TryToGetFromDefaultCulture = true;
    }
}
