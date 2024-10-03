using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.EventBus;
using Tiknas.EventBus.Dapr;
using Tiknas.EventBus.Distributed;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.Dapr.EventBus;

[DependsOn(
    typeof(TiknasAspNetCoreMvcDaprModule),
    typeof(TiknasEventBusDaprModule)
)]
public class TiknasAspNetCoreMvcDaprEventBusModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var subscribeOptions = context.Services.ExecutePreConfiguredActions<TiknasSubscribeOptions>();

        Configure<TiknasEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                var rootServiceProvider = endpointContext.ScopeServiceProvider.GetRequiredService<IRootServiceProvider>();
                subscribeOptions.SubscriptionsCallback = subscriptions =>
                {
                    var daprEventBusOptions = rootServiceProvider.GetRequiredService<IOptions<TiknasDaprEventBusOptions>>().Value;
                    foreach (var handler in rootServiceProvider.GetRequiredService<IOptions<TiknasDistributedEventBusOptions>>().Value.Handlers)
                    {
                        foreach (var @interface in handler.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDistributedEventHandler<>)))
                        {
                            var eventType = @interface.GetGenericArguments()[0];
                            var eventName = EventNameAttribute.GetNameOrDefault(eventType);

                            if (subscriptions.Any(x => x.PubsubName == daprEventBusOptions.PubSubName && x.Topic == eventName))
                            {
                                // Controllers with a [Topic] attribute can replace built-in event handlers.
                                continue;
                            }

                            var subscription = new TiknasSubscription
                            {
                                PubsubName = daprEventBusOptions.PubSubName,
                                Topic = eventName,
                                Route = TiknasAspNetCoreMvcDaprPubSubConsts.DaprEventCallbackUrl,
                                Metadata = new TiknasMetadata
                                {
                                    {
                                        TiknasMetadata.RawPayload, "true"
                                    }
                                }
                            };
                            subscriptions.Add(subscription);
                        }
                    }

                    return Task.CompletedTask;
                };

                endpointContext.Endpoints.MapTiknasSubscribeHandler(subscribeOptions);
            });
        });
    }
}
