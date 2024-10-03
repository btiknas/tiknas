using Microsoft.Extensions.DependencyInjection;
using Tiknas.Kafka;
using Tiknas.Modularity;

namespace Tiknas.EventBus.Kafka;

[DependsOn(
    typeof(TiknasEventBusModule),
    typeof(TiknasKafkaModule))]
public class TiknasEventBusKafkaModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasKafkaEventBusOptions>(configuration.GetSection("Kafka:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<KafkaDistributedEventBus>()
            .Initialize();
    }
}
