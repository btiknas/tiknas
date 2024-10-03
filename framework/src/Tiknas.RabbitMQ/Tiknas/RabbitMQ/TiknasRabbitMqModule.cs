using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.RabbitMQ;

[DependsOn(
    typeof(TiknasJsonModule),
    typeof(TiknasThreadingModule)
    )]
public class TiknasRabbitMqModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<TiknasRabbitMqOptions>(configuration.GetSection("RabbitMQ"));
        Configure<TiknasRabbitMqOptions>(options =>
        {
            foreach (var connectionFactory in options.Connections.Values)
            {
                connectionFactory.DispatchConsumersAsync = true;
            }
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        context.ServiceProvider
            .GetRequiredService<IChannelPool>()
            .Dispose();

        context.ServiceProvider
            .GetRequiredService<IConnectionPool>()
            .Dispose();
    }
}
