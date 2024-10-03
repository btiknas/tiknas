using MongoDB.Bson.Serialization;

namespace Tiknas.MongoDB;

public interface IHasBsonClassMap
{
    BsonClassMap GetMap();
}
