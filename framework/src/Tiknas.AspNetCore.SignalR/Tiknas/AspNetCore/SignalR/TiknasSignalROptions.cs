using System;

namespace Tiknas.AspNetCore.SignalR;

public class TiknasSignalROptions
{
    public HubConfigList Hubs { get; }

    /// <summary>
    /// Default: 5 seconds.
    /// </summary>
    public TimeSpan? CheckDynamicClaimsInterval { get; set; }

    public TiknasSignalROptions()
    {
        Hubs = new HubConfigList();
        CheckDynamicClaimsInterval = TimeSpan.FromSeconds(5);
    }
}
