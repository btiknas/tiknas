using MongoDB.Driver;

namespace Tiknas.MongoDB.TestApp.ThirdDbContext;

public interface IThirdDbContext : ITiknasMongoDbContext
{
    IMongoCollection<ThirdDbContextDummyEntity> DummyEntities { get; }
}
