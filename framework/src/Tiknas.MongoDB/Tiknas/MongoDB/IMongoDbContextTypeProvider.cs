using System;

namespace Tiknas.MongoDB;

public interface IMongoDbContextTypeProvider
{
    Type GetDbContextType(Type dbContextType);
}
