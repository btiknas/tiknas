using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tiknas.Json.Newtonsoft;

public class TiknasCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
{
    private readonly TiknasDateTimeConverter _dateTimeConverter;

    public TiknasCamelCasePropertyNamesContractResolver(TiknasDateTimeConverter dateTimeConverter)
    {
        _dateTimeConverter = dateTimeConverter;

        NamingStrategy = new CamelCaseNamingStrategy
        {
            ProcessDictionaryKeys = false
        };
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
