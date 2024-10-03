using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Data;
using Tiknas.Modularity;
using Tiknas.MongoDB.TestApp.FifthContext;
using Tiknas.MongoDB.TestApp.SecondContext;
using Tiknas.MongoDB.TestApp.ThirdDbContext;
using Tiknas.MultiTenancy;
using Tiknas.TestApp;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.MongoDb;
using Tiknas.TestApp.MongoDB;

namespace Tiknas.MongoDB;

[DependsOn(
    typeof(TestAppModule),
    typeof(TiknasMongoDbTestSecondContextModule)
    )]
public class TiknasMongoDbTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });

        context.Services.AddMongoDbContext<TestAppMongoDbContext>(options =>
        {
            options.AddDefaultRepositories<ITestAppMongoDbContext>();
            options.AddRepository<City, CityRepository>();

            options.ReplaceDbContext<IThirdDbContext>();
        });

        context.Services.AddMongoDbContext<HostTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories<IFifthDbContext>();
            options.ReplaceDbContext<IFifthDbContext>(MultiTenancySides.Host);
        });

        context.Services.AddMongoDbContext<TenantTestAppDbContext>(options =>
        {
            options.AddDefaultRepositories<IFifthDbContext>();
        });
    }
}
