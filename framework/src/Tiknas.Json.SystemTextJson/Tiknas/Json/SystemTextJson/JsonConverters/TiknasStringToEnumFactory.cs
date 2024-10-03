using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tiknas.Json.SystemTextJson.JsonConverters;

public class TiknasStringToEnumFactory : JsonConverterFactory
{
    private readonly JsonNamingPolicy? _namingPolicy;
    private readonly bool _allowIntegerValues;

    public TiknasStringToEnumFactory()
        : this(namingPolicy: null, allowIntegerValues: true)
    {

    }

    public TiknasStringToEnumFactory(JsonNamingPolicy? namingPolicy, bool allowIntegerValues)
    {
        _namingPolicy = namingPolicy;
        _allowIntegerValues = allowIntegerValues;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter)Activator.CreateInstance(
            typeof(TiknasStringToEnumConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            new object?[] { _namingPolicy, _allowIntegerValues },
            culture: null)!;
    }
}
