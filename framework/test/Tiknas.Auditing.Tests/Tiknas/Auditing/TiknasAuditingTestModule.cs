using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Auditing.App.Entities;
using Tiknas.Auditing.App.EntityFrameworkCore;
using Tiknas.Autofac;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.Sqlite;
using Tiknas.Modularity;

namespace Tiknas.Auditing;

[DependsOn(
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasEntityFrameworkCoreSqliteModule)
)]
public class TiknasAuditingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTiknasDbContext<TiknasAuditingTestDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.Entity<AppEntityWithNavigations>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.OneToOne).Include(p => p.OneToMany).Include(p => p.ManyToMany);
            });
        });

        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<TiknasDbContextOptions>(options =>
        {
            options.Configure(tiknasDbContextConfigurationContext =>
            {
                tiknasDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });

        Configure<TiknasAuditingOptions>(options =>
        {
            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithSelector",
                    type => type == typeof(AppEntityWithSelector))
            );

            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithSoftDelete",
                    type => type == typeof(AppEntityWithSoftDelete))
            );

            options.EntityHistorySelectors.Add(
                new NamedTypeSelector(
                    "AppEntityWithValueObject",
                    type => type == typeof(AppEntityWithValueObject) || type == typeof(AppEntityWithValueObjectAddress))
            );
        });

        context.Services.AddType<Auditing_Tests.MyAuditedObject1>();
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new TiknasUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        using (var context = new TiknasAuditingTestDbContext(new DbContextOptionsBuilder<TiknasAuditingTestDbContext>()
            .UseSqlite(connection).AddTiknasDbContextOptionsExtension().Options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
}
