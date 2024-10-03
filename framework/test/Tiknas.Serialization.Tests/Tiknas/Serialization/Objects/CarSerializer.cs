using System.Text.Json;
using Tiknas.DependencyInjection;

namespace Tiknas.Serialization.Objects;

public class CarSerializer : IObjectSerializer<Car>, ITransientDependency
{
    public byte[] Serialize(Car obj)
    {
        obj.Name += "-serialized";
        return JsonSerializer.SerializeToUtf8Bytes(obj);
    }

    public Car Deserialize(byte[] bytes)
    {
        var car = JsonSerializer.Deserialize<Car>(bytes);
        car.Name += "-deserialized";
        return car;
    }
}
