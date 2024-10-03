using System;

namespace Tiknas.EventBus.Rebus;

public interface IRebusSerializer
{
    byte[] Serialize(object obj);

    object Deserialize(byte[] value, Type type);

    T Deserialize<T>(byte[] value);
}
