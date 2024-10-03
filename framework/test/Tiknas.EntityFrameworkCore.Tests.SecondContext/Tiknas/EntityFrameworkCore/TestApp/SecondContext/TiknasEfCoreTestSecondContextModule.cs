using Microsoft.Extensions.DependencyInjection;
using Tiknas.EntityFrameworkCore.TestApp.FourthContext;
using Tiknas.EntityFrameworkCore.TestApp.ThirdDbContext;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.EntityFrameworkCore.TestApp.SecondContext;

[DependsOn(typeof(TiknasEntityFrameworkCoreModule))]
public class TiknasEfCoreTestSecondContextModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTiknasDbContext<SecondDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        context.Services.AddTiknasDbContext<ThirdDbContext.ThirdDbContext>(options =>
        {
            options.AddDefaultRepositories<IThirdDbContext>();
        });

        context.Services.AddTiknasDbContext<FourthDbContext>(options =>
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
