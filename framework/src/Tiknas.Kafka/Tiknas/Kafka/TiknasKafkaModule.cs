using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.Kafka;

[DependsOn(
    typeof(TiknasJsonModule),
    typeof(TiknasThreadingModule)
)]
public class TiknasKafkaModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<TiknasKafkaOptions>(configuration.GetSection("Kafka"));
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        context.ServiceProvider
            .GetRequiredService<IConsumerPool>()
            .Dispose();

        context.ServiceProvider
            .GetRequiredService<IProducerPool>()
            .Dispose();
    }
}
