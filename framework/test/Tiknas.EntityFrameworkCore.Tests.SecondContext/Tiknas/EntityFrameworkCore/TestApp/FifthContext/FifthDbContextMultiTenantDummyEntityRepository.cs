using System;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.FifthContext;

public interface IFifthDbContextMultiTenantDummyEntityRepository : IBasicRepository<FifthDbContextMultiTenantDummyEntity, Guid>
{

}

public class FifthDbContextMultiTenantDummyEntityRepository :
    EfCoreRepository<IFifthDbContext, FifthDbContextMultiTenantDummyEntity, Guid>,
    IFifthDbContextMultiTenantDummyEntityRepository
{
    public FifthDbContextMultiTenantDummyEntityRepository(IDbContextProvider<IFifthDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
