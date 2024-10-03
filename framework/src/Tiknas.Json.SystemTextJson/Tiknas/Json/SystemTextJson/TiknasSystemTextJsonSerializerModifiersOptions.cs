using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using Tiknas.Json.SystemTextJson.Modifiers;


namespace Tiknas.Json.SystemTextJson;

public class TiknasSystemTextJsonSerializerModifiersOptions
{
    public List<Action<JsonTypeInfo>> Modifiers { get; }

    public TiknasSystemTextJsonSerializerModifiersOptions()
    {
        Modifiers = new List<Action<JsonTypeInfo>>
        {
            TiknasIncludeExtraPropertiesModifiers.Modify,
        };
    }
}
