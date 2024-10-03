using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;

public interface IThirdDbContext : IEfCoreDbContext
{
    DbSet<ThirdDbContextDummyEntity> DummyEntities { get; set; }
}
