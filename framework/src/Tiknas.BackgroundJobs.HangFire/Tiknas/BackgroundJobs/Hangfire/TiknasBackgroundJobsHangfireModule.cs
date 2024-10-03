using System;
using System.Linq;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Hangfire;
using Tiknas.Modularity;

namespace Tiknas.BackgroundJobs.Hangfire;

[DependsOn(
    typeof(TiknasBackgroundJobsAbstractionsModule),
    typeof(TiknasHangfireModule)
)]
public class TiknasBackgroundJobsHangfireModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(serviceProvider =>
            serviceProvider.GetRequiredService<TiknasDashboardOptionsProvider>().Get());
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundJobOptions>>().Value;
        if (!options.IsJobExecutionEnabled)
        {
            var hangfireOptions = context.ServiceProvider.GetRequiredService<IOptions<TiknasHangfireOptions>>().Value;
            hangfireOptions.BackgroundJobServerFactory = CreateOnlyEnqueueJobServer;
        }
    }

    private BackgroundJobServer? CreateOnlyEnqueueJobServer(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<JobStorage>();
        return null;
    }
}
