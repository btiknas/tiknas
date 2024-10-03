using System.Text.Json;
using Tiknas.Collections;

namespace Tiknas.Json.SystemTextJson;

public class TiknasSystemTextJsonSerializerOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; }

    public TiknasSystemTextJsonSerializerOptions()
    {
        JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
    }
}
