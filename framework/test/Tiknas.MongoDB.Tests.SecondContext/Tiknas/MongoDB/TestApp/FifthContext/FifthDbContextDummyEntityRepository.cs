using System;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.MongoDB;

namespace Tiknas.MongoDB.TestApp.FifthContext;

public interface IFifthDbContextDummyEntityRepository : IBasicRepository<FifthDbContextDummyEntity, Guid>
{
    Task<ITiknasMongoDbContext> GetDbContextAsync();
}

public class FifthDbContextDummyEntityRepository :
    MongoDbRepository<IFifthDbContext, FifthDbContextDummyEntity, Guid>,
    IFifthDbContextDummyEntityRepository
{
    public FifthDbContextDummyEntityRepository(IMongoDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<ITiknasMongoDbContext> GetDbContextAsync()
    {
        return await base.GetDbContextAsync();
    }
}
