using Microsoft.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;

namespace Tiknas.EntityFrameworkCore.TestApp.FourthContext;

/* This dbcontext is just for testing to replace dbcontext from the application using ReplaceDbContextAttribute
 */
public class FourthDbContext : TiknasDbContext<FourthDbContext>, IFourthDbContext
{
    public DbSet<FourthDbContextDummyEntity> FourthDummyEntities { get; set; }

    public FourthDbContext(DbContextOptions<FourthDbContext> options)
        : base(options)
    {
    }
}
