using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.FifthContext;

public interface IFifthDbContext : IEfCoreDbContext
{
    DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }

    DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }
}
