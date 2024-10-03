using System;
using JetBrains.Annotations;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public interface ITiknasDbContextRegistrationOptionsBuilder : ITiknasCommonDbContextRegistrationOptionsBuilder
{
    void Entity<TEntity>([NotNull] Action<TiknasEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity;
}
