using System.Threading;
using System.Threading.Tasks;

namespace Tiknas.Uow;

public interface ISupportsRollback
{
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
