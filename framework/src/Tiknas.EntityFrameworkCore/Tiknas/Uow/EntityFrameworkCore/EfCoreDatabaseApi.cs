using System.Threading;
using System.Threading.Tasks;
using Tiknas.EntityFrameworkCore;

namespace Tiknas.Uow.EntityFrameworkCore;

public class EfCoreDatabaseApi : IDatabaseApi, ISupportsSavingChanges
{
    public IEfCoreDbContext DbContext { get; }

    public EfCoreDatabaseApi(IEfCoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
