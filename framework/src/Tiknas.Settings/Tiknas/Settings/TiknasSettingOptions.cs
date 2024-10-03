using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.Settings;

public class TiknasSettingOptions
{
    public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<ISettingValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedSettings { get; }

    public TiknasSettingOptions()
    {
        DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
        ValueProviders = new TypeList<ISettingValueProvider>();
        DeletedSettings = new HashSet<string>();
    }
}
