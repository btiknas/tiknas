﻿using System;
using Tiknas.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionRegistrationActionExtensions
{
    // OnRegistered

    public static void OnRegistered(this IServiceCollection services, Action<IOnServiceRegistredContext> registrationAction)
    {
        GetOrCreateRegistrationActionList(services).Add(registrationAction);
    }

    public static ServiceRegistrationActionList GetRegistrationActionList(this IServiceCollection services)
    {
        return GetOrCreateRegistrationActionList(services);
    }

    private static ServiceRegistrationActionList GetOrCreateRegistrationActionList(IServiceCollection services)
    {
        var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceRegistrationActionList>>()?.Value;
        if (actionList == null)
        {
            actionList = new ServiceRegistrationActionList();
            services.AddObjectAccessor(actionList);
        }

        return actionList;
    }

    public static void DisableTiknasClassInterceptors(this IServiceCollection services)
    {
        GetOrCreateRegistrationActionList(services).IsClassInterceptorsDisabled = true;
    }

    public static bool IsTiknasClassInterceptorsDisabled(this IServiceCollection services)
    {
        return GetOrCreateRegistrationActionList(services).IsClassInterceptorsDisabled;
    }

    // OnExposing

    public static void OnExposing(this IServiceCollection services, Action<IOnServiceExposingContext> exposeAction)
    {
        GetOrCreateExposingList(services).Add(exposeAction);
    }

    public static ServiceExposingActionList GetExposingActionList(this IServiceCollection services)
    {
        return GetOrCreateExposingList(services);
    }

    private static ServiceExposingActionList GetOrCreateExposingList(IServiceCollection services)
    {
        var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceExposingActionList>>()?.Value;
        if (actionList == null)
        {
            actionList = new ServiceExposingActionList();
            services.AddObjectAccessor(actionList);
        }

        return actionList;
    }
}
