using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Hangfire;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.BackgroundWorkers.Hangfire;

[DependsOn(
    typeof(TiknasBackgroundWorkersModule),
    typeof(TiknasHangfireModule))]
public class TiknasBackgroundWorkersHangfireModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(typeof(HangfirePeriodicBackgroundWorkerAdapter<>));
    }
    
    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasBackgroundWorkerOptions>>().Value;
        if (!options.IsEnabled)
        {
            var hangfireOptions = context.ServiceProvider.GetRequiredService<IOptions<TiknasHangfireOptions>>().Value;
            hangfireOptions.BackgroundJobServerFactory = CreateOnlyEnqueueJobServer;
        }
        
        context.ServiceProvider
            .GetRequiredService<HangfireBackgroundWorkerManager>()
            .Initialize(); 
    }
    
    private BackgroundJobServer? CreateOnlyEnqueueJobServer(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<JobStorage>();
        return null;
    }
}
