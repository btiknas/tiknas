using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tiknas.EntityFrameworkCore.DependencyInjection;

namespace Tiknas.EntityFrameworkCore;

public static class TiknasDbContextConfigurationContextSqlServerExtensions
{
    public static DbContextOptionsBuilder UseSqlServer(
        [NotNull] this TiknasDbContextConfigurationContext context,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        if (context.ExistingConnection != null)
        {
            return context.DbContextOptions.UseSqlServer(context.ExistingConnection, optionsBuilder =>
            {
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlServerOptionsAction?.Invoke(optionsBuilder);
            });
        }
        else
        {
            return context.DbContextOptions.UseSqlServer(context.ConnectionString, optionsBuilder =>
            {
                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlServerOptionsAction?.Invoke(optionsBuilder);
            });
        }
    }
}
