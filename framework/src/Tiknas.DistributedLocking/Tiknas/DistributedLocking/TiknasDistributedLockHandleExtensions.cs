using System;
using Medallion.Threading;

namespace Tiknas.DistributedLocking;

public static class TiknasDistributedLockHandleExtensions
{
    public static IDistributedSynchronizationHandle ToDistributedSynchronizationHandle(
        this ITiknasDistributedLockHandle handle)
    {
        return handle.As<MedallionTiknasDistributedLockHandle>().Handle;
    }
}
