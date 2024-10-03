using System;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.GlobalFeatures;

public static class GlobalFeatureInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (ShouldIntercept(context.ImplementationType))
        {
            context.Interceptors.TryAdd<GlobalFeatureInterceptor>();
        }
    }

    private static bool ShouldIntercept(Type type)
    {
        return !DynamicProxyIgnoreTypes.Contains(type) && typeof(IGlobalFeatureCheckingEnabled).IsAssignableFrom(type);
    }
}
