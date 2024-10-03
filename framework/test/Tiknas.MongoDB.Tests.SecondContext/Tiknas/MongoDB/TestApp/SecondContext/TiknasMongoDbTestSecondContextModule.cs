using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.MongoDB.TestApp.FourthContext;
using Tiknas.MongoDB.TestApp.ThirdDbContext;
using Tiknas.Threading;

namespace Tiknas.MongoDB.TestApp.SecondContext;

[DependsOn(typeof(TiknasMongoDbModule))]
public class TiknasMongoDbTestSecondContextModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SecondDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        context.Services.AddMongoDbContext<ThirdDbContext.ThirdDbContext>(options =>
        {
            options.AddDefaultRepositories<IThirdDbContext>();
        });

        context.Services.AddMongoDbContext<FourthDbContext>(options =>
        {
            options.AddDefaultRepositories<IFourthDbContext>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<SecondContextTestDataBuilder>()
                .BuildAsync());
        }
    }
}
