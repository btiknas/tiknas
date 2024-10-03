using MongoDB.Driver;
using Tiknas.DependencyInjection;
using Tiknas.MongoDB;
using Tiknas.MongoDB.TestApp.FifthContext;
using Tiknas.MultiTenancy;

namespace Tiknas.TestApp.MongoDb;

[ReplaceDbContext(typeof(IFifthDbContext), MultiTenancySides.Tenant)]
public class TenantTestAppDbContext : TiknasMongoDbContext, IFifthDbContext
{
    public IMongoCollection<FifthDbContextDummyEntity> FifthDbContextDummyEntity => Collection<FifthDbContextDummyEntity>();

    public IMongoCollection<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity => Collection<FifthDbContextMultiTenantDummyEntity>();
}
