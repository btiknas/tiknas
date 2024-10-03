using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.BackgroundWorkers;
using Tiknas.Data;
using Tiknas.DistributedLocking;
using Tiknas.Guids;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Threading;
using Tiknas.Timing;

namespace Tiknas.BackgroundJobs;

[DependsOn(
    typeof(TiknasBackgroundJobsAbstractionsModule),
    typeof(TiknasBackgroundWorkersModule),
    typeof(TiknasTimingModule),
    typeof(TiknasGuidsModule),
    typeof(TiknasDistributedLockingAbstractionsModule),
    typeof(TiknasMultiTenancyModule)
    )]
public class TiknasBackgroundJobsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (context.Services.IsDataMigrationEnvironment())
        {
            Configure<TiknasBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        if (context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundJobOptions>>().Value.IsJobExecutionEnabled)
        {
            await context.AddBackgroundWorkerAsync<IBackgroundJobWorker>();
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }
}
