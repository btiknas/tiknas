using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Routing;

public class TiknasEndpointRouterOptions
{
    public List<Action<EndpointRouteBuilderContext>> EndpointConfigureActions { get; }

    public TiknasEndpointRouterOptions()
    {
        EndpointConfigureActions = new List<Action<EndpointRouteBuilderContext>>();
    }
}
