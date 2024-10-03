using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.MongoDB.DependencyInjection;

public class TiknasMongoDbConventionalRegistrar : DefaultConventionalRegistrar
{
    protected override bool IsConventionalRegistrationDisabled(Type type)
    {
        return !typeof(ITiknasMongoDbContext).IsAssignableFrom(type) || type == typeof(TiknasMongoDbContext) || base.IsConventionalRegistrationDisabled(type);
    }

    protected override List<Type> GetExposedServiceTypes(Type type)
    {
        return new List<Type>()
            {
                typeof(ITiknasMongoDbContext)
            };
    }

    protected override ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
    {
        return ServiceLifetime.Transient;
    }
}
