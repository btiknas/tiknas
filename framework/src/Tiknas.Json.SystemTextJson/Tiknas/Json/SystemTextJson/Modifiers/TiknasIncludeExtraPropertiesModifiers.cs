using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using Tiknas.Data;
using Tiknas.ObjectExtending;

namespace Tiknas.Json.SystemTextJson.Modifiers;

public static class TiknasIncludeExtraPropertiesModifiers
{
    public static void Modify(JsonTypeInfo jsonTypeInfo)
    {
        if (typeof(IHasExtraProperties).IsAssignableFrom(jsonTypeInfo.Type))
        {
            var propertyJsonInfo = jsonTypeInfo.Properties
                .Where(x => x.AttributeProvider is MemberInfo)
                .FirstOrDefault(x =>
                    x.PropertyType == typeof(ExtraPropertyDictionary) &&
                    x.AttributeProvider!.As<MemberInfo>().Name == nameof(ExtensibleObject.ExtraProperties) &&
                    x.Set == null);

            if (propertyJsonInfo != null)
            {
                propertyJsonInfo.Set = (obj, value) =>
                {
                    ObjectHelper.TrySetProperty(obj.As<IHasExtraProperties>(), x => x.ExtraProperties, () => value);
                };
            }
        }
    }
}
