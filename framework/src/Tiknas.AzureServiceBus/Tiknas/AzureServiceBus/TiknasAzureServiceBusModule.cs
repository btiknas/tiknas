using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.Threading;

namespace Tiknas.AzureServiceBus;

[DependsOn(
    typeof(TiknasJsonModule),
    typeof(TiknasThreadingModule)
)]
public class TiknasAzureServiceBusModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<TiknasAzureServiceBusOptions>(configuration.GetSection("Azure:ServiceBus"));
    }
}
