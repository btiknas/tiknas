using MongoDB.Driver;

namespace Tiknas.MongoDB.TestApp.FifthContext;

public class FifthDbContext : TiknasMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }
}
