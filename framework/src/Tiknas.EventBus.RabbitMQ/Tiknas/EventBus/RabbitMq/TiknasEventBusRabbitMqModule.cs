using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.RabbitMQ;

namespace Tiknas.EventBus.RabbitMq;

[DependsOn(
    typeof(TiknasEventBusModule),
    typeof(TiknasRabbitMqModule))]
public class TiknasEventBusRabbitMqModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasRabbitMqEventBusOptions>(configuration.GetSection("RabbitMQ:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<RabbitMqDistributedEventBus>()
            .Initialize();
    }
}
