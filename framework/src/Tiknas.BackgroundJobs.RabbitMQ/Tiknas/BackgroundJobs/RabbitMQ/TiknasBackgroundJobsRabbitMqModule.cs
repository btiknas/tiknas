using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.RabbitMQ;
using Tiknas.Threading;

namespace Tiknas.BackgroundJobs.RabbitMQ;

[DependsOn(
    typeof(TiknasBackgroundJobsAbstractionsModule),
    typeof(TiknasRabbitMqModule),
    typeof(TiknasThreadingModule)
)]
public class TiknasBackgroundJobsRabbitMqModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(IJobQueue<>), typeof(JobQueue<>));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await StartJobQueueManagerAsync(context);
    }

    public async override Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
    {
        await StopJobQueueManagerAsync(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationShutdownAsync(context));
    }

    private async static Task StartJobQueueManagerAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IJobQueueManager>()
            .StartAsync();
    }

    private async static Task StopJobQueueManagerAsync(ApplicationShutdownContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<IJobQueueManager>()
            .StopAsync();
    }
}
