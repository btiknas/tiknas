using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.BackgroundWorkers;
using Tiknas.DistributedLocking;
using Tiknas.EventBus.Abstractions;
using Tiknas.EventBus.Distributed;
using Tiknas.EventBus.Local;
using Tiknas.Guids;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Reflection;
using Tiknas.Threading;

namespace Tiknas.EventBus;

[DependsOn(
    typeof(TiknasEventBusAbstractionsModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasJsonModule),
    typeof(TiknasGuidsModule),
    typeof(TiknasBackgroundWorkersModule),
    typeof(TiknasDistributedLockingAbstractionsModule)
    )]
public class TiknasEventBusModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AddEventHandlers(context.Services);
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<OutboxSenderManager>();
        await context.AddBackgroundWorkerAsync<InboxProcessManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    private static void AddEventHandlers(IServiceCollection services)
    {
        var localHandlers = new List<Type>();
        var distributedHandlers = new List<Type>();

        services.OnRegistered(context =>
        {
            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(ILocalEventHandler<>)))
            {
                localHandlers.Add(context.ImplementationType);
            }

            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IDistributedEventHandler<>)))
            {
                distributedHandlers.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasLocalEventBusOptions>(options =>
        {
            options.Handlers.AddIfNotContains(localHandlers);
        });

        services.Configure<TiknasDistributedEventBusOptions>(options =>
        {
            options.Handlers.AddIfNotContains(distributedHandlers);
        });

    }
}
