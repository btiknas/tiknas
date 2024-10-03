using System;

namespace Tiknas.AspNetCore.SignalR;

public interface ITiknasHubContextAccessor
{
    TiknasHubContext Context { get; }

    IDisposable Change(TiknasHubContext context);
}

