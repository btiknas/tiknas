using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Options;

namespace Tiknas.Json.SystemTextJson;

public class TiknasDefaultJsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public TiknasDefaultJsonTypeInfoResolver(IOptions<TiknasSystemTextJsonSerializerModifiersOptions> options)
    {
        foreach (var modifier in options.Value.Modifiers)
        {
            Modifiers.Add(modifier);
        }
    }
}
