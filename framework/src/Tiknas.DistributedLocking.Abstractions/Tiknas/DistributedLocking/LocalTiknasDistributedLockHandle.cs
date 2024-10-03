using System;
using System.Threading.Tasks;

namespace Tiknas.DistributedLocking;

public class LocalTiknasDistributedLockHandle : ITiknasDistributedLockHandle
{
    private readonly IDisposable _disposable;

    public LocalTiknasDistributedLockHandle(IDisposable disposable)
    {
        _disposable = disposable;
    }

    public ValueTask DisposeAsync()
    {
        _disposable.Dispose();
        return default;
    }
}
