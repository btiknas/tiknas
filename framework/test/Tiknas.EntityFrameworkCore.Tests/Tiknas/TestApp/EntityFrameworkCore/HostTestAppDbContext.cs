using Microsoft.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.TestApp.FifthContext;

namespace Tiknas.TestApp.EntityFrameworkCore;

public class HostTestAppDbContext : TiknasDbContext<HostTestAppDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }
    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public HostTestAppDbContext(DbContextOptions<HostTestAppDbContext> options)
        : base(options)
    {
    }
}
