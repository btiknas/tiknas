using Microsoft.Extensions.DependencyInjection;
using Tiknas.Dapr;
using Tiknas.Modularity;

namespace Tiknas.EventBus.Dapr;

[DependsOn(
    typeof(TiknasEventBusModule),
    typeof(TiknasDaprModule)
    )]
public class TiknasEventBusDaprModule : TiknasModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<DaprDistributedEventBus>()
            .Initialize();
    }
}
