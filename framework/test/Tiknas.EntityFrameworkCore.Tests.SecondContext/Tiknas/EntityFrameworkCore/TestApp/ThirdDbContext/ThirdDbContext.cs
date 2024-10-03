using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;

/* This dbcontext is just for testing to replace dbcontext from the application using TiknasDbContextRegistrationOptions.ReplaceDbContext
 */
public class ThirdDbContext : TiknasDbContext<ThirdDbContext>, IThirdDbContext
{
    public DbSet<ThirdDbContextDummyEntity> DummyEntities { get; set; }

    public ThirdDbContext(DbContextOptions<ThirdDbContext> options)
        : base(options)
    {
    }
}
