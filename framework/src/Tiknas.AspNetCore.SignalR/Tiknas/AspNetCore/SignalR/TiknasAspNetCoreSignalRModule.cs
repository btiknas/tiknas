﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Auditing;
using Tiknas.AspNetCore.SignalR.Auditing;
using Tiknas.AspNetCore.SignalR.Authentication;
using Tiknas.Auditing;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.SignalR;

[DependsOn(
    typeof(TiknasAspNetCoreModule)
    )]
public class TiknasAspNetCoreSignalRModule : TiknasModule
{
    private static readonly MethodInfo MapHubGenericMethodInfo =
        typeof(TiknasAspNetCoreSignalRModule)
            .GetMethod("MapHub", BindingFlags.Static | BindingFlags.NonPublic)!;

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasSignalRConventionalRegistrar());

        AutoAddHubTypes(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var routePatterns = new List<string> { "/signalr-hubs" };
        var signalRServerBuilder = context.Services.AddSignalR(options =>
        {
            options.DisableImplicitFromServicesParameters = true;
            options.AddFilter<TiknasHubContextAccessorHubFilter>();
            options.AddFilter<TiknasAuthenticationHubFilter>();
            options.AddFilter<TiknasAuditHubFilter>();
        });

        context.Services.ExecutePreConfiguredActions(signalRServerBuilder);

        Configure<TiknasEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                var signalROptions = endpointContext
                    .ScopeServiceProvider
                    .GetRequiredService<IOptions<TiknasSignalROptions>>()
                    .Value;

                var hubWithRoutePatterns = new List<KeyValuePair<Type, string>>();
                foreach (var hubConfig in signalROptions.Hubs)
                {
                    routePatterns.AddIfNotContains(hubConfig.RoutePattern);

                    if (hubWithRoutePatterns.Any(x => x.Key == hubConfig.HubType && x.Value == hubConfig.RoutePattern))
                    {
                        throw new TiknasException($"The hub type {hubConfig.HubType.FullName} is already registered with route pattern {hubConfig.RoutePattern}");
                    }

                    hubWithRoutePatterns.Add(new KeyValuePair<Type, string>(hubConfig.HubType, hubConfig.RoutePattern));
                    MapHubType(
                        hubConfig.HubType,
                        endpointContext.Endpoints,
                        hubConfig.RoutePattern,
                        opts =>
                        {
                            foreach (var configureAction in hubConfig.ConfigureActions)
                            {
                                configureAction(opts);
                            }
                        }
                    );
                }
            });
        });

        Configure<TiknasAspNetCoreAuditingOptions>(options =>
        {
            foreach (var routePattern in routePatterns)
            {
                options.IgnoredUrls.AddIfNotContains(x => routePattern.StartsWith(x, StringComparison.OrdinalIgnoreCase), () => routePattern);
            }
        });

        Configure<TiknasAuditingOptions>(options =>
        {
            options.Contributors.Add(new AspNetCoreSignalRAuditLogContributor());
        });
    }

    private void AutoAddHubTypes(IServiceCollection services)
    {
        var hubTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (IsHubClass(context) && !IsDisabledForAutoMap(context))
            {
                hubTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasSignalROptions>(options =>
        {
            foreach (var hubType in hubTypes)
            {
                options.Hubs.Add(HubConfig.Create(hubType));
            }
        });
    }

    private static bool IsHubClass(IOnServiceRegistredContext context)
    {
        return typeof(Hub).IsAssignableFrom(context.ImplementationType);
    }

    private static bool IsDisabledForAutoMap(IOnServiceRegistredContext context)
    {
        return context.ImplementationType.IsDefined(typeof(DisableAutoHubMapAttribute), true);
    }

    private void MapHubType(
        Type hubType,
        IEndpointRouteBuilder endpoints,
        string pattern,
        Action<HttpConnectionDispatcherOptions> configureOptions)
    {
        MapHubGenericMethodInfo
            .MakeGenericMethod(hubType)
            .Invoke(
                this,
                new object[]
                {
                        endpoints,
                        pattern,
                        configureOptions
                }
            );
    }

    // ReSharper disable once UnusedMember.Local (used via reflection)
    private static void MapHub<THub>(
        IEndpointRouteBuilder endpoints,
        string pattern,
        Action<HttpConnectionDispatcherOptions> configureOptions)
        where THub : Hub
    {
        endpoints.MapHub<THub>(
            pattern,
            configureOptions
        );
    }
}
