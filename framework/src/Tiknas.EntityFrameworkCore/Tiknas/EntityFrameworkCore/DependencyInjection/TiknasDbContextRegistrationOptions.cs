using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public class TiknasDbContextRegistrationOptions : TiknasCommonDbContextRegistrationOptions, ITiknasDbContextRegistrationOptionsBuilder
{
    public Dictionary<Type, object> TiknasEntityOptions { get; }

    public TiknasDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
        : base(originalDbContextType, services)
    {
        TiknasEntityOptions = new Dictionary<Type, object>();
    }

    public void Entity<TEntity>(Action<TiknasEntityOptions<TEntity>> optionsAction) where TEntity : IEntity
    {
        Services.Configure<TiknasEntityOptions>(options =>
        {
            options.Entity(optionsAction);
        });
    }
}
