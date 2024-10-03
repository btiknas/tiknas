using Tiknas.MongoDB;

namespace Tiknas.Uow.MongoDB;

public class MongoDbDatabaseApi : IDatabaseApi
{
    public ITiknasMongoDbContext DbContext { get; }

    public MongoDbDatabaseApi(ITiknasMongoDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
