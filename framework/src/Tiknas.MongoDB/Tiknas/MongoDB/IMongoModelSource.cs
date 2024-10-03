namespace Tiknas.MongoDB;

public interface IMongoModelSource
{
    MongoDbContextModel GetModel(TiknasMongoDbContext dbContext);
}
