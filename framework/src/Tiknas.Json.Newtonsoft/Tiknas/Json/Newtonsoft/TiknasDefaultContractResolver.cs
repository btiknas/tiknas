using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tiknas.Json.Newtonsoft;

public class TiknasDefaultContractResolver : DefaultContractResolver
{
    private readonly TiknasDateTimeConverter _dateTimeConverter;

    public TiknasDefaultContractResolver(TiknasDateTimeConverter dateTimeConverter)
    {
        _dateTimeConverter = dateTimeConverter;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (TiknasDateTimeConverter.ShouldNormalize(member, property))
        {
            property.Converter = _dateTimeConverter;
        }

        return property;
    }
}
