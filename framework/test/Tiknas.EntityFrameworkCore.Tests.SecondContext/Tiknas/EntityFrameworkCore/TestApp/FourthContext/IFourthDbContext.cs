using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.FourthContext;

public interface IFourthDbContext : IEfCoreDbContext
{
    DbSet<FourthDbContextDummyEntity> FourthDummyEntities { get; set; }
}
