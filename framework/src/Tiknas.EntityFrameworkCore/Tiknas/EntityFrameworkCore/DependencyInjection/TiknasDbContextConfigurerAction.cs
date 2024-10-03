using System;
using JetBrains.Annotations;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public class TiknasDbContextConfigurerAction : ITiknasDbContextConfigurer
{
    [NotNull]
    public Action<TiknasDbContextConfigurationContext> Action { get; }

    public TiknasDbContextConfigurerAction([NotNull] Action<TiknasDbContextConfigurationContext> action)
    {
        Check.NotNull(action, nameof(action));

        Action = action;
    }

    public void Configure(TiknasDbContextConfigurationContext context)
    {
        Action.Invoke(context);
    }
}

public class TiknasDbContextConfigurerAction<TDbContext> : TiknasDbContextConfigurerAction
    where TDbContext : TiknasDbContext<TDbContext>
{
    public TiknasDbContextConfigurerAction([NotNull] Action<TiknasDbContextConfigurationContext> action)
        : base(action)
    {
    }
}
