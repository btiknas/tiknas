using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Tiknas;
using Tiknas.Autofac;
using Tiknas.Castle.DynamicProxy;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;
using Tiknas.Modularity;

namespace Autofac.Builder;

public static class TiknasRegistrationBuilderExtensions
{
    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> ConfigureTiknasConventions<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
            ServiceDescriptor serviceDescriptor,
            IModuleContainer moduleContainer,
            ServiceRegistrationActionList registrationActionList,
            ServiceActivatedActionList activatedActionList)
        where TActivatorData : ReflectionActivatorData
    {
        registrationBuilder = registrationBuilder.InvokeActivatedActions(activatedActionList, serviceDescriptor);

        var serviceType = registrationBuilder.RegistrationData.Services.OfType<IServiceWithType>().FirstOrDefault()?.ServiceType;
        if (serviceType == null)
        {
            return registrationBuilder;
        }

        var implementationType = registrationBuilder.ActivatorData.ImplementationType;
        if (implementationType == null)
        {
            return registrationBuilder;
        }

        registrationBuilder = registrationBuilder.EnablePropertyInjection(moduleContainer, implementationType);
        registrationBuilder = registrationBuilder.InvokeRegistrationActions(registrationActionList, serviceType, implementationType);

        return registrationBuilder;
    }

    private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InvokeActivatedActions<TLimit, TActivatorData, TRegistrationStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
        ServiceActivatedActionList activatedActionList,
        ServiceDescriptor serviceDescriptor)
        where TActivatorData : ReflectionActivatorData
    {
        var actions = activatedActionList.GetActions(serviceDescriptor);
        if (actions.Any())
        {
            registrationBuilder.OnActivated(context =>
            {
                var serviceActivatedContext = new OnServiceActivatedContext(context.Instance!);
                foreach (var action in actions)
                {
                    action.Invoke(serviceActivatedContext);
                }
            });
        }

        return registrationBuilder;
    }

    private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InvokeRegistrationActions<TLimit, TActivatorData, TRegistrationStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
        ServiceRegistrationActionList registrationActionList,
        Type serviceType,
        Type implementationType)
        where TActivatorData : ReflectionActivatorData
    {
        var serviceRegistredArgs = new OnServiceRegistredContext(serviceType, implementationType);

        foreach (var registrationAction in registrationActionList)
        {
            registrationAction.Invoke(serviceRegistredArgs);
        }

        if (serviceRegistredArgs.Interceptors.Any())
        {
            var disableTiknasFeaturesAttribute = serviceRegistredArgs.ImplementationType.GetCustomAttribute<DisableTiknasFeaturesAttribute>(true);
            if (disableTiknasFeaturesAttribute == null || !disableTiknasFeaturesAttribute.DisableInterceptors)
            {
                registrationBuilder = registrationBuilder.AddInterceptors(
                    registrationActionList,
                    serviceType,
                    serviceRegistredArgs.Interceptors
                );
            }
        }

        return registrationBuilder;
    }

    private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> EnablePropertyInjection<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
            IModuleContainer moduleContainer,
            Type implementationType)
        where TActivatorData : ReflectionActivatorData
    {
        // Enable Property Injection only for types in an assembly containing an TiknasModule and without a DisablePropertyInjection attribute on class or properties.
        if (moduleContainer.Modules.Any(m => m.AllAssemblies.Contains(implementationType.Assembly)) &&
            implementationType.GetCustomAttributes(typeof(DisablePropertyInjectionAttribute), true).IsNullOrEmpty())
        {
            registrationBuilder = registrationBuilder.PropertiesAutowired(new TiknasPropertySelector(false));
        }

        return registrationBuilder;
    }

    private static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        AddInterceptors<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder,
            ServiceRegistrationActionList serviceRegistrationActionList,
            Type serviceType,
            IEnumerable<Type> interceptors)
        where TActivatorData : ReflectionActivatorData
    {
        if (serviceType.IsInterface)
        {
            registrationBuilder = registrationBuilder.EnableInterfaceInterceptors();
        }
        else
        {
            if (serviceRegistrationActionList.IsClassInterceptorsDisabled)
            {
                return registrationBuilder;
            }

            (registrationBuilder as IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, TRegistrationStyle>)?.EnableClassInterceptors();
        }

        foreach (var interceptor in interceptors)
        {
            registrationBuilder.InterceptedBy(
                typeof(TiknasAsyncDeterminationInterceptor<>).MakeGenericType(interceptor)
            );
        }

        return registrationBuilder;
    }
}
