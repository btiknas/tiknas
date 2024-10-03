using System.Text;
using Tiknas.DependencyInjection;
using Tiknas.Json;

namespace Tiknas.Caching;

public class Utf8JsonDistributedCacheSerializer : IDistributedCacheSerializer, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }

    public Utf8JsonDistributedCacheSerializer(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj!));
    }

    public T Deserialize<T>(byte[] bytes)
    {
        return (T)JsonSerializer.Deserialize(typeof(T), Encoding.UTF8.GetString(bytes));
    }
}
