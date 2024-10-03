using System;

namespace Tiknas.DistributedLocking.Dapr;

public class TiknasDistributedLockDaprOptions
{
    public string StoreName { get; set; } = default!;

    public string? Owner { get; set; }

    public TimeSpan DefaultExpirationTimeout { get; set; }

    public TiknasDistributedLockDaprOptions()
    {
        DefaultExpirationTimeout = TimeSpan.FromMinutes(2);
    }
}
