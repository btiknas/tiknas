using System;
using JetBrains.Annotations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Tiknas.EntityFrameworkCore;

public static class TiknasDbContextOptionsPostgreSqlExtensions
{
    [Obsolete("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
    public static void UsePostgreSql(
        [NotNull] this TiknasDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseNpgsql(postgreSqlOptionsAction);
        });
    }

    [Obsolete("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
    public static void UsePostgreSql<TDbContext>(
        [NotNull] this TiknasDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseNpgsql(postgreSqlOptionsAction);
        });
    }

    public static void UseNpgsql(
        [NotNull] this TiknasDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseNpgsql(postgreSqlOptionsAction);
        });
    }

    public static void UseNpgsql<TDbContext>(
        [NotNull] this TiknasDbContextOptions options,
        Action<NpgsqlDbContextOptionsBuilder>? postgreSqlOptionsAction = null)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseNpgsql(postgreSqlOptionsAction);
        });
    }
}
