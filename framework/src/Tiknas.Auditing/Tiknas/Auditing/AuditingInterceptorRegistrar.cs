﻿using System;
using System.Linq;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.Auditing;

public static class AuditingInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (ShouldIntercept(context.ImplementationType))
        {
            context.Interceptors.TryAdd<AuditingInterceptor>();
        }
    }

    private static bool ShouldIntercept(Type type)
    {
        if (DynamicProxyIgnoreTypes.Contains(type))
        {
            return false;
        }

        if (ShouldAuditTypeByDefaultOrNull(type, ignoreIntegrationServiceAttribute: true) == true)
        {
            return true;
        }

        if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true)))
        {
            return true;
        }

        return false;
    }

    //TODO: Move to a better place
    public static bool? ShouldAuditTypeByDefaultOrNull(Type type, bool ignoreIntegrationServiceAttribute)
    {
        //TODO: In an inheritance chain, it would be better to check the attributes on the top class first.

        if (type.IsDefined(typeof(AuditedAttribute), true))
        {
            return true;
        }

        if (type.IsDefined(typeof(DisableAuditingAttribute), true))
        {
            return false;
        }

        if (typeof(IAuditingEnabled).IsAssignableFrom(type))
        {
            if (ignoreIntegrationServiceAttribute || 
                !IntegrationServiceAttribute.IsDefinedOrInherited(type))
            {
                return true;
            }
        }

        return null;
    }
}
