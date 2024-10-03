using JetBrains.Annotations;
using System;
using Devart.Data.Oracle.Entity;

namespace Tiknas.EntityFrameworkCore;

public static class TiknasDbContextOptionsOracleDevartExtensions
{
    public static void UseOracle(
            [NotNull] this TiknasDbContextOptions options,
            Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null,
            bool useExistingConnectionIfAvailable = false)
    {
        options.Configure(context =>
        {
            context.UseOracle(oracleOptionsAction, useExistingConnectionIfAvailable);
        });
    }

    public static void UseOracle<TDbContext>(
        [NotNull] this TiknasDbContextOptions options,
        Action<OracleDbContextOptionsBuilder>? oracleOptionsAction = null,
        bool useExistingConnectionIfAvailable = false)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        options.Configure<TDbContext>(context =>
        {
            context.UseOracle(oracleOptionsAction, useExistingConnectionIfAvailable);
        });
    }
}
