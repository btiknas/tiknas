using MongoDB.Driver;

namespace Tiknas.MongoDB.TestApp.FifthContext;

public interface IFifthDbContext : ITiknasMongoDbContext
{
    IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; }

    IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; }
}
