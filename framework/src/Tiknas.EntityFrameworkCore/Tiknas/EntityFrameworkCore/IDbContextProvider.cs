using System;
using System.Threading.Tasks;

namespace Tiknas.EntityFrameworkCore;

public interface IDbContextProvider<TDbContext>
    where TDbContext : IEfCoreDbContext
{
    [Obsolete("Use GetDbContextAsync method.")]
    TDbContext GetDbContext();

    Task<TDbContext> GetDbContextAsync();
}
