using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AzureServiceBus;
using Tiknas.Modularity;

namespace Tiknas.EventBus.Azure;

[DependsOn(
    typeof(TiknasEventBusModule),
    typeof(TiknasAzureServiceBusModule)
)]
public class TiknasEventBusAzureModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasAzureEventBusOptions>(configuration.GetSection("Azure:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasAzureEventBusOptions>>().Value;

        if (!options.IsServiceBusDisabled)
        {
            context
                .ServiceProvider
                .GetRequiredService<AzureDistributedEventBus>()
                .Initialize();
        }

    }
}
