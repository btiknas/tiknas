using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiknas.DependencyInjection;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public class TiknasDbContextConfigurationContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public string ConnectionString { get; }

    public string? ConnectionStringName { get; }

    public DbConnection? ExistingConnection { get; }

    public DbContextOptionsBuilder DbContextOptions { get; protected set; }

    public TiknasDbContextConfigurationContext(
        [NotNull] string connectionString,
        [NotNull] IServiceProvider serviceProvider,
        string? connectionStringName,
        DbConnection? existingConnection)
    {
        ConnectionString = connectionString;
        ServiceProvider = serviceProvider;
        ConnectionStringName = connectionStringName;
        ExistingConnection = existingConnection;

        DbContextOptions = new DbContextOptionsBuilder()
            .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
            .UseApplicationServiceProvider(serviceProvider);
    }
}

public class TiknasDbContextConfigurationContext<TDbContext> : TiknasDbContextConfigurationContext
    where TDbContext : TiknasDbContext<TDbContext>
{
    public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

    public TiknasDbContextConfigurationContext(
        string connectionString,
        [NotNull] IServiceProvider serviceProvider,
        string? connectionStringName,
        DbConnection? existingConnection)
        : base(
              connectionString,
              serviceProvider,
              connectionStringName,
              existingConnection)
    {
        base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>()
            .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
            .UseApplicationServiceProvider(serviceProvider); ;
    }
}
