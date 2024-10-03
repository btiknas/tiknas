using System.Threading.Tasks;
using Medallion.Threading;

namespace Tiknas.DistributedLocking;

public class MedallionTiknasDistributedLockHandle : ITiknasDistributedLockHandle
{
    public IDistributedSynchronizationHandle Handle { get; }

    public MedallionTiknasDistributedLockHandle(IDistributedSynchronizationHandle handle)
    {
        Handle = handle;
    }

    public ValueTask DisposeAsync()
    {
        return Handle.DisposeAsync();
    }
}
