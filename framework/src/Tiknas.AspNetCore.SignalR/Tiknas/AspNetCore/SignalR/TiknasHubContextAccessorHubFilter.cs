using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Tiknas.AspNetCore.SignalR;

public class TiknasHubContextAccessorHubFilter : IHubFilter
{
    public virtual async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var hubContextAccessor = invocationContext.ServiceProvider.GetRequiredService<ITiknasHubContextAccessor>();
        using (hubContextAccessor.Change(new TiknasHubContext(
                   invocationContext.ServiceProvider,
                   invocationContext.Hub,
                   invocationContext.HubMethod,
                   invocationContext.HubMethodArguments)))
        {
            return await next(invocationContext);
        }
    }
}
