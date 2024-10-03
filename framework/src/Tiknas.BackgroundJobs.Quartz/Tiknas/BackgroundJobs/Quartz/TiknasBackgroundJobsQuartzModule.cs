using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Modularity;
using Tiknas.Quartz;

namespace Tiknas.BackgroundJobs.Quartz;

[DependsOn(
    typeof(TiknasBackgroundJobsAbstractionsModule),
    typeof(TiknasQuartzModule)
)]
public class TiknasBackgroundJobsQuartzModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(QuartzJobExecutionAdapter<>));
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundJobOptions>>().Value;
        if (!options.IsJobExecutionEnabled)
        {
            var quartzOptions = context.ServiceProvider.GetRequiredService<IOptions<TiknasQuartzOptions>>().Value;
            quartzOptions.StartSchedulerFactory = scheduler => Task.CompletedTask;
        }
    }
}
