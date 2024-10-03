using System;

namespace Tiknas.Domain.Repositories.MemoryDb;

public interface IMemoryDbSerializer
{
    byte[] Serialize(object obj);

    object Deserialize(byte[] value, Type type);
}
