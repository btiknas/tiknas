using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tiknas.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasEfCoreDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddTiknasDbContextOptionsExtension(this DbContextOptionsBuilder optionsBuilder)
    {
        ((IDbContextOptionsBuilderInfrastructure) optionsBuilder).AddOrUpdateExtension(new TiknasDbContextOptionsExtension());
        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> AddTiknasDbContextOptionsExtension<TContext>(this DbContextOptionsBuilder<TContext> optionsBuilder)
        where TContext : DbContext
    {
        ((IDbContextOptionsBuilderInfrastructure) optionsBuilder).AddOrUpdateExtension(new TiknasDbContextOptionsExtension());
        return optionsBuilder;
    }
}
