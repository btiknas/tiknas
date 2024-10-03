using JetBrains.Annotations;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Tiknas.EntityFrameworkCore;

public static class TiknasDbContextOptionsMySQLExtensions
{
    public static void UseMySQL(
            [NotNull] this TiknasDbContextOptions options,
            Action<MySqlDbContextOptionsBuilder>? mySQLOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseMySQL(mySQLOptionsAction);
        });
    }

    public static void UseMySQL<TDbContext>(
        [NotNull] this TiknasDbContextOptions options,
        Action<MySqlDbContextOptionsBuilder>? mySQLOptionsAction = null)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseMySQL(mySQLOptionsAction);
        });
    }
}
