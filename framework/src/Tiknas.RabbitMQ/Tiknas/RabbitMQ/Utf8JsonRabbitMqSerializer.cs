using System;
using System.Text;
using Tiknas.DependencyInjection;
using Tiknas.Json;

namespace Tiknas.RabbitMQ;

public class Utf8JsonRabbitMqSerializer : IRabbitMqSerializer, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;

    public Utf8JsonRabbitMqSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public byte[] Serialize(object obj)
    {
        return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(obj));
    }

    public object Deserialize(byte[] value, Type type)
    {
        return _jsonSerializer.Deserialize(type, Encoding.UTF8.GetString(value));
    }

    public T Deserialize<T>(byte[] value)
    {
        return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value));
    }
}
