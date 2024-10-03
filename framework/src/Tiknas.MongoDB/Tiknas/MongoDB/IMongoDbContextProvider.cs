using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tiknas.MongoDB;

public interface IMongoDbContextProvider<TMongoDbContext>
    where TMongoDbContext : ITiknasMongoDbContext
{
    [Obsolete("Use CreateDbContextAsync")]
    TMongoDbContext GetDbContext();

    Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default);
}
