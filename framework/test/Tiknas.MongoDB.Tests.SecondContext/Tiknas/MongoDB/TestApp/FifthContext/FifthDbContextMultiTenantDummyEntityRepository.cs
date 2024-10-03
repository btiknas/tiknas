using System;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.MongoDB;

namespace Tiknas.MongoDB.TestApp.FifthContext;

public interface IFifthDbContextMultiTenantDummyEntityRepository : IBasicRepository<FifthDbContextMultiTenantDummyEntity, Guid>
{
    Task<ITiknasMongoDbContext> GetDbContextAsync();
}

public class FifthDbContextMultiTenantDummyEntityRepository :
    MongoDbRepository<IFifthDbContext, FifthDbContextMultiTenantDummyEntity, Guid>,
    IFifthDbContextMultiTenantDummyEntityRepository
{
    public FifthDbContextMultiTenantDummyEntityRepository(IMongoDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<ITiknasMongoDbContext> GetDbContextAsync()
    {
        return await base.GetDbContextAsync();
    }
}
