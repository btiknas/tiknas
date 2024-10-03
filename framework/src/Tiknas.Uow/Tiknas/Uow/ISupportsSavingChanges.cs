using System.Threading;
using System.Threading.Tasks;

namespace Tiknas.Uow;

public interface ISupportsSavingChanges
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
