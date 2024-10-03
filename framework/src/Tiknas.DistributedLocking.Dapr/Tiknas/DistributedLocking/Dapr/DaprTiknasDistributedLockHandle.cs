using System.Threading.Tasks;
using Dapr.Client;

namespace Tiknas.DistributedLocking.Dapr;

public class DaprTiknasDistributedLockHandle : ITiknasDistributedLockHandle
{
    protected TryLockResponse LockResponse { get; }

    public DaprTiknasDistributedLockHandle(TryLockResponse lockResponse)
    {
        LockResponse = lockResponse;
    }

    public async ValueTask DisposeAsync()
    {
        await LockResponse.DisposeAsync();
    }
}
