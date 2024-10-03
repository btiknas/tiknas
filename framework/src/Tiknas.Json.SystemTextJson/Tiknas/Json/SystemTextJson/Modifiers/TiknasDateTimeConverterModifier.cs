using System;
using System.Linq;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json.SystemTextJson.JsonConverters;
using Tiknas.Reflection;
using Tiknas.Timing;

namespace Tiknas.Json.SystemTextJson.Modifiers;

public class TiknasDateTimeConverterModifier
{
    private readonly TiknasDateTimeConverter _tiknasDateTimeConverter;
    private readonly TiknasNullableDateTimeConverter _tiknasNullableDateTimeConverter;

    public TiknasDateTimeConverterModifier(TiknasDateTimeConverter tiknasDateTimeConverter, TiknasNullableDateTimeConverter tiknasNullableDateTimeConverter)
    {
        _tiknasDateTimeConverter = tiknasDateTimeConverter;
        _tiknasNullableDateTimeConverter = tiknasNullableDateTimeConverter;
    }

    public Action<JsonTypeInfo> CreateModifyAction()
    {
        return Modify;
    }

    private void Modify(JsonTypeInfo jsonTypeInfo)
    {
        if (ReflectionHelper.GetAttributesOfMemberOrDeclaringType<DisableDateTimeNormalizationAttribute>(jsonTypeInfo.Type).Any())
        {
            return;
        }

        foreach (var property in jsonTypeInfo.Properties.Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?)))
        {
            if (property.AttributeProvider == null ||
                !property.AttributeProvider.GetCustomAttributes(typeof(DisableDateTimeNormalizationAttribute), false).Any())
            {
                property.CustomConverter = property.PropertyType == typeof(DateTime)
                    ? _tiknasDateTimeConverter
                    : _tiknasNullableDateTimeConverter;
            }
        }
    }
}
