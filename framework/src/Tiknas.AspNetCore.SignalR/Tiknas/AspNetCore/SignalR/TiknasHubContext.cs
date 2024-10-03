using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.SignalR;

namespace Tiknas.AspNetCore.SignalR;

public class TiknasHubContext
{
    public IServiceProvider ServiceProvider { get; }

    public Hub Hub { get; }

    public MethodInfo HubMethod { get; }

    public IReadOnlyList<object?> HubMethodArguments { get; }

    public TiknasHubContext(IServiceProvider serviceProvider, Hub hub, MethodInfo hubMethod, IReadOnlyList<object?> hubMethodArguments)
    {
        ServiceProvider = serviceProvider;
        Hub = hub;
        HubMethod = hubMethod;
        HubMethodArguments = hubMethodArguments;
    }
}
