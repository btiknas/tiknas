using System;
using System.Reflection;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.Domain.ChangeTracking;

public class ChangeTrackingInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (ShouldIntercept(context.ImplementationType))
        {
            context.Interceptors.TryAdd<ChangeTrackingInterceptor>();
        }
    }

    private static bool ShouldIntercept(Type type)
    {
        return !DynamicProxyIgnoreTypes.Contains(type) && ChangeTrackingHelper.IsEntityChangeTrackingType(type.GetTypeInfo());
    }
}
