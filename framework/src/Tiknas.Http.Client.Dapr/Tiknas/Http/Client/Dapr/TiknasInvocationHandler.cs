using System;
using Dapr.Client;
using Microsoft.Extensions.Options;
using Tiknas.Dapr;
using Tiknas.DependencyInjection;

namespace Tiknas.Http.Client.Dapr;

public class TiknasInvocationHandler : InvocationHandler, ITransientDependency
{
    public TiknasInvocationHandler(IOptions<TiknasDaprOptions> daprOptions)
    {
        if (!daprOptions.Value.HttpEndpoint.IsNullOrWhiteSpace())
        {
            DaprEndpoint = daprOptions.Value.HttpEndpoint!;
        }
    }
}
