using System;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories.MemoryDb;

namespace Tiknas.MemoryDb;

public interface IMemoryDatabaseProvider<TMemoryDbContext>
    where TMemoryDbContext : MemoryDbContext
{
    [Obsolete("Use GetDbContextAsync method.")]
    TMemoryDbContext DbContext { get; }

    Task<TMemoryDbContext> GetDbContextAsync();

    [Obsolete("Use GetDatabaseAsync method.")]
    IMemoryDatabase GetDatabase();

    Task<IMemoryDatabase> GetDatabaseAsync();
}
