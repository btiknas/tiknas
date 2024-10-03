using MongoDB.Driver;

namespace Tiknas.MongoDB;

public interface ITiknasMongoDbContext
{
    IMongoClient Client { get; }

    IMongoDatabase Database { get; }

    IMongoCollection<T> Collection<T>();

    IClientSessionHandle? SessionHandle { get; }
}
