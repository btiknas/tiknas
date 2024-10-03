using System;
using System.Reflection;

namespace Tiknas.Http.ProxyScripting.Configuration;

public static class TiknasApiProxyScriptingConfiguration
{
    public static Func<PropertyInfo, string?> PropertyNameGenerator { get; set; }

    static TiknasApiProxyScriptingConfiguration()
    {
        PropertyNameGenerator = propertyInfo =>
            propertyInfo.GetSingleAttributeOrNull<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name;
    }
}
