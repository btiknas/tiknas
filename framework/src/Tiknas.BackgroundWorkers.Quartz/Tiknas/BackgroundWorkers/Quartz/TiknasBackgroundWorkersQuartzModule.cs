using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Tiknas.Modularity;
using Tiknas.Quartz;
using Tiknas.Threading;

namespace Tiknas.BackgroundWorkers.Quartz;

[DependsOn(
    typeof(TiknasBackgroundWorkersModule),
    typeof(TiknasQuartzModule)
)]
public class TiknasBackgroundWorkersQuartzModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasQuartzConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(QuartzPeriodicBackgroundWorkerAdapter<>));
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundWorkerOptions>>().Value;
        if (!options.IsEnabled)
        {
            var quartzOptions = context.ServiceProvider.GetRequiredService<IOptions<TiknasQuartzOptions>>().Value;
            quartzOptions.StartSchedulerFactory = _ => Task.CompletedTask;
        }
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var quartzBackgroundWorkerOptions = context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundWorkerQuartzOptions>>().Value;
        if (quartzBackgroundWorkerOptions.IsAutoRegisterEnabled)
        {
            var backgroundWorkerManager = context.ServiceProvider.GetRequiredService<IBackgroundWorkerManager>();
            var works = context.ServiceProvider.GetServices<IQuartzBackgroundWorker>().Where(x => x.AutoRegister);

            foreach (var work in works)
            {
                await backgroundWorkerManager.AddAsync(work);
            }
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }
}
