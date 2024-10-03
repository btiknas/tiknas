using Microsoft.Extensions.DependencyInjection;
using Tiknas.Authorization.Permissions;
using Tiknas.Authorization.TestServices;
using Tiknas.Autofac;
using Tiknas.DynamicProxy;
using Tiknas.ExceptionHandling;
using Tiknas.Modularity;

namespace Tiknas.Authorization;

[DependsOn(typeof(TiknasAutofacModule))]
[DependsOn(typeof(TiknasAuthorizationModule))]
[DependsOn(typeof(TiknasExceptionHandlingModule))]
public class TiknasAuthorizationTestModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(onServiceRegistredContext =>
        {
            if (typeof(IMyAuthorizedService1).IsAssignableFrom(onServiceRegistredContext.ImplementationType) &&
                !DynamicProxyIgnoreTypes.Contains(onServiceRegistredContext.ImplementationType))
            {
                onServiceRegistredContext.Interceptors.TryAdd<AuthorizationInterceptor>();
            }
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasPermissionOptions>(options =>
        {
            options.ValueProviders.Add<TestPermissionValueProvider1>();
            options.ValueProviders.Add<TestPermissionValueProvider2>();
        });
    }
}
