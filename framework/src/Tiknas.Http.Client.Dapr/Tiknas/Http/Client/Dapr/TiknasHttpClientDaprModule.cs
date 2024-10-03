using Microsoft.Extensions.DependencyInjection;
using Tiknas.Dapr;
using Tiknas.Modularity;

namespace Tiknas.Http.Client.Dapr;

[DependsOn(
    typeof(TiknasHttpClientModule),
    typeof(TiknasDaprModule)
)]
public class TiknasHttpClientDaprModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<TiknasHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((_, clientBuilder) =>
            {
                clientBuilder.AddHttpMessageHandler<TiknasInvocationHandler>();
            });
        });
    }
}