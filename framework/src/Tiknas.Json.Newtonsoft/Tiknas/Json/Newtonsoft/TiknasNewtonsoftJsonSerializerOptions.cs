using Newtonsoft.Json;

namespace Tiknas.Json.Newtonsoft;

public class TiknasNewtonsoftJsonSerializerOptions
{
    public JsonSerializerSettings JsonSerializerSettings { get; }

    public TiknasNewtonsoftJsonSerializerOptions()
    {
        JsonSerializerSettings = new JsonSerializerSettings();
    }
}
