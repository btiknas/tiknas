using System;
using System.Collections.Generic;

namespace Tiknas.MongoDB;

public class MongoDbContextModel
{
    public IReadOnlyDictionary<Type, IMongoEntityModel> Entities { get; }

    public MongoDbContextModel(IReadOnlyDictionary<Type, IMongoEntityModel> entities)
    {
        Entities = entities;
    }
}
