using JetBrains.Annotations;
using System;
using Oracle.EntityFrameworkCore.Infrastructure;

namespace Tiknas.EntityFrameworkCore;

public static class TiknasDbContextOptionsOracleExtensions
{
    public static void UseOracle(
            [NotNull] this TiknasDbContextOptions options,
            Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null)
    {
        options.Configure(context =>
        {
            context.UseOracle(oracleOptionsAction);
        });
    }

    public static void UseOracle<TDbContext>(
        [NotNull] this TiknasDbContextOptions options,
        Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseOracle(oracleOptionsAction);
        });
    }
}
