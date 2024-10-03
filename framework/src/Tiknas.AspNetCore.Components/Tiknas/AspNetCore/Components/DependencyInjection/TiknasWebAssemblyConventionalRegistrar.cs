using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.DependencyInjection;

public class TiknasWebAssemblyConventionalRegistrar : DefaultConventionalRegistrar
{
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !IsComponent(type) || base.IsConventionalRegistrationDisabled(type);
    }

    private static bool IsComponent(Type type)
    {
        return typeof(ComponentBase).IsAssignableFrom(type);
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}
