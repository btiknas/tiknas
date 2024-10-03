using Microsoft.EntityFrameworkCore;
using Tiknas.DependencyInjection;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.TestApp.FifthContext;
using Tiknas.MultiTenancy;

namespace Tiknas.TestApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IFifthDbContext), MultiTenancySides.Tenant)]
public class TenantTestAppDbContext : TiknasDbContext<TenantTestAppDbContext>, IFifthDbContext
{
    public DbSet<FifthDbContextDummyEntity> FifthDbContextDummyEntity { get; set; }
    public DbSet<FifthDbContextMultiTenantDummyEntity> FifthDbContextMultiTenantDummyEntity { get; set; }

    public TenantTestAppDbContext(DbContextOptions<TenantTestAppDbContext> options)
        : base(options)
    {
    }
}
