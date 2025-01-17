using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.FifthContext;

public class FifthDbContext : TiknasDbContext<FifthDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }

    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public FifthDbContext(DbContextOptions<FifthDbContext> options)
        : base(options)
    {
    }
}
