﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.SignalR.Authentication;

public class TiknasAuthenticationHubFilter : IHubFilter
{
    public virtual async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var currentPrincipalAccessor = invocationContext.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        var claimsPrincipal = invocationContext.Context.User;
        await HandleDynamicClaimsPrincipalAsync(claimsPrincipal, invocationContext.ServiceProvider, invocationContext.Context, false);
        using (currentPrincipalAccessor.Change(claimsPrincipal!))
        {
            return await next(invocationContext);
        }
    }

    public virtual async Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        var currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        var claimsPrincipal = context.Context.User;
        await HandleDynamicClaimsPrincipalAsync(claimsPrincipal, context.ServiceProvider, context.Context, true);
        using (currentPrincipalAccessor.Change(claimsPrincipal!))
        {
            await next(context);
        }
    }

    public virtual async Task OnDisconnectedAsync(HubLifetimeContext context, Exception? exception, Func<HubLifetimeContext, Exception?, Task> next)
    {
        var currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        var claimsPrincipal = context.Context.User;
        await HandleDynamicClaimsPrincipalAsync(claimsPrincipal, context.ServiceProvider, context.Context, true);
        using (currentPrincipalAccessor.Change(claimsPrincipal!))
        {
            await next(context, exception);
        }
    }

    protected virtual async Task HandleDynamicClaimsPrincipalAsync(ClaimsPrincipal? claimsPrincipal, IServiceProvider serviceProvider, HubCallerContext hubCallerContext, bool skipCheckDynamicClaimsInterval)
    {
        if (claimsPrincipal?.Identity != null &&
            claimsPrincipal.Identity.IsAuthenticated &&
            serviceProvider.GetRequiredService<IOptions<TiknasClaimsPrincipalFactoryOptions>>().Value
                .IsDynamicClaimsEnabled)
        {
            var checkDynamicClaimsInterval = serviceProvider.GetRequiredService<IOptions<TiknasSignalROptions>>().Value.CheckDynamicClaimsInterval;
            if (!skipCheckDynamicClaimsInterval &&
                checkDynamicClaimsInterval.HasValue &&
                hubCallerContext.Items.TryGetValue(nameof(HandleDynamicClaimsPrincipalAsync), out var lastCheckDynamicClaimsTime) &&
                lastCheckDynamicClaimsTime is DateTime lastCheckDynamicClaimsTimeValue)
            {
                if (DateTime.UtcNow.Subtract(lastCheckDynamicClaimsTimeValue) < checkDynamicClaimsInterval.Value)
                {
                    // Dynamic claims are not checked because the interval has not passed yet.
                    return;
                }
            }

            hubCallerContext.Items[nameof(HandleDynamicClaimsPrincipalAsync)] = DateTime.UtcNow;

            claimsPrincipal = claimsPrincipal.Identity is ClaimsIdentity identity
                ? new ClaimsPrincipal(new ClaimsIdentity(claimsPrincipal.Claims,
                    claimsPrincipal.Identity.AuthenticationType, identity.NameClaimType, identity.RoleClaimType))
                : new ClaimsPrincipal(new ClaimsIdentity(claimsPrincipal.Claims,
                    claimsPrincipal.Identity.AuthenticationType));

            claimsPrincipal = await serviceProvider.GetRequiredService<ITiknasClaimsPrincipalFactory>()
                .CreateDynamicAsync(claimsPrincipal);
            if (claimsPrincipal.Identity?.IsAuthenticated == false)
            {
                hubCallerContext.Abort();
            }
        }
    }
}
