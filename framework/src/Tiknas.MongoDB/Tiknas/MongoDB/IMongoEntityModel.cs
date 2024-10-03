using System;

namespace Tiknas.MongoDB;

public interface IMongoEntityModel
{
    Type EntityType { get; }

    string CollectionName { get; }
}
