using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Tiknas.Domain.Entities;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public class TiknasEntityOptions<TEntity>
    where TEntity : IEntity
{
    public static TiknasEntityOptions<TEntity> Empty { get; } = new TiknasEntityOptions<TEntity>();

    public Func<IQueryable<TEntity>, IQueryable<TEntity>>? DefaultWithDetailsFunc { get; set; }
}

public class TiknasEntityOptions
{
    private readonly IDictionary<Type, object> _options;

    public TiknasEntityOptions()
    {
        _options = new Dictionary<Type, object>();
    }

    public TiknasEntityOptions<TEntity>? GetOrNull<TEntity>()
        where TEntity : IEntity
    {
        return _options.GetOrDefault(typeof(TEntity)) as TiknasEntityOptions<TEntity>;
    }

    public void Entity<TEntity>([NotNull] Action<TiknasEntityOptions<TEntity>> optionsAction)
        where TEntity : IEntity
    {
        Check.NotNull(optionsAction, nameof(optionsAction));

        optionsAction(
            (_options.GetOrAdd(
                typeof(TEntity),
                () => new TiknasEntityOptions<TEntity>()
            ) as TiknasEntityOptions<TEntity>)!
        );
    }
}
