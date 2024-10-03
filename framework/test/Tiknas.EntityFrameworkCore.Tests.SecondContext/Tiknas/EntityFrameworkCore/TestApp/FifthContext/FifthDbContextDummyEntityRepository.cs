using System;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.FifthContext;

public interface IFifthDbContextDummyEntityRepository : IBasicRepository<FifthDbContextDummyEntity, Guid>
{

}

public class FifthDbContextDummyEntityRepository :
    EfCoreRepository<IFifthDbContext, FifthDbContextDummyEntity, Guid>,
    IFifthDbContextDummyEntityRepository
{
    public FifthDbContextDummyEntityRepository(IDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
