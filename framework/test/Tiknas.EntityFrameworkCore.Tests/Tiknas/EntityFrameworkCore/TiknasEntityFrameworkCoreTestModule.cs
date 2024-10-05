using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Autofac;
using Tiknas.Domain.Repositories;
using Tiknas.EntityFrameworkCore.Domain;
using Tiknas.EntityFrameworkCore.Sqlite;
using Tiknas.EntityFrameworkCore.TestApp.FifthContext;
using Tiknas.EntityFrameworkCore.TestApp.SecondContext;
using Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.TestApp;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.EntityFrameworkCore;
using Tiknas.Threading;

namespace Tiknas.EntityFrameworkCore;

[DependsOn(typeof(TiknasEntityFrameworkCoreSqliteModule))]
[DependsOn(typeof(TestAppModule))]
[DependsOn(typeof(TiknasAutofacModule))]
[DependsOn(typeof(TiknasEfCoreTestSecondContextModule))]
public class TiknasEntityFrameworkCoreTestModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        TestEntityExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTiknasDbContext<TestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.ReplaceDbContext<IThirdDbContext>();

            options.Entity<Person>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.Phones);
            });

            options.Entity<Author>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.Books);
            });

            options.Entity<AppEntityWithNavigations>(opt =>
            {
                opt.DefaultWithDetailsFunc = q => q.Include(p => p.OneToOne).ThenInclude(x => x.OneToOne).Include(p => p.OneToMany).ThenInclude(x => x.OneToMany).Include(p => p.ManyToMany);
            });
        });

        context.Services.AddTiknasDbContext<HostTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.ReplaceDbContext<IFifthDbContext>(MultiTenancySides.Host);
        });

        context.Services.AddTiknasDbContext<TenantTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });

        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<TiknasDbContextOptions>(options =>
        {
            options.Configure(tiknasDbContextConfigurationContext =>
            {
                tiknasDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection).AddTiknasDbContextOptionsExtension();
            });
        });
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        context.ServiceProvider.GetRequiredService<SecondDbContext>().Database.Migrate();
        using (var scope = context.ServiceProvider.CreateScope())
        {
            var categoryRepository = scope.ServiceProvider.GetRequiredService<IBasicRepository<Category, Guid>>();
            AsyncHelper.RunSync(async () =>
            {
                await categoryRepository.InsertManyAsync(new List<Category>
                {
                    new Category { Name = "tiknas" },
                    new Category { Name = "tiknas.cli" },
                    new Category { Name = "tiknas.core", IsDeleted = true }
                });
            });
        }
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new TiknasUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        using (var context = new TestMigrationsDbContext(new DbContextOptionsBuilder<TestMigrationsDbContext>().UseSqlite(connection).AddTiknasDbContextOptionsExtension().Options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
            context.Database.ExecuteSqlRaw(
                @"CREATE VIEW View_PersonView AS 
                      SELECT Name, CreationTime, Birthday, LastActive FROM People");
        }

        return connection;
    }
}
