using MongoDB.Bson;
using MongoDB.Driver;

namespace Tiknas.MongoDB;

public interface IHasCreateCollectionOptions
{
    CreateCollectionOptions<BsonDocument> CreateCollectionOptions { get; }
}