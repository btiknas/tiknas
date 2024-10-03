using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.AutoMapper;

public class TiknasAutoMapperConventionalRegistrar : DefaultConventionalRegistrar
{
    protected readonly Type[] OpenTypes = {
            typeof(IValueResolver<,,>),
            typeof(IMemberValueResolver<,,,>),
            typeof(ITypeConverter<,>),
            typeof(IValueConverter<,>),
            typeof(IMappingAction<,>)
        };

    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !type.GetInterfaces().Any(x => x.IsGenericType && OpenTypes.Contains(x.GetGenericTypeDefinition())) ||
               base.IsConventionalRegistrationDisabled(type);
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}
