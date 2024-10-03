using MongoDB.Driver;
using Tiknas.MongoDB;
using Tiknas.MongoDB.TestApp.FifthContext;

namespace Tiknas.TestApp.MongoDb;

public class HostTestAppDbContext : TiknasMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity => Collection<FifthDbContextDummyEntity>();

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity => Collection<FifthDbContextMultiTenantDummyEntity>();
}
