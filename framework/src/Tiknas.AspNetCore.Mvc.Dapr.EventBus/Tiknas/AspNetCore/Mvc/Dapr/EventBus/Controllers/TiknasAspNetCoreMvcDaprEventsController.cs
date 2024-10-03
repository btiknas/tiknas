using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiknas.Dapr;
using Tiknas.EventBus.Dapr;

namespace Tiknas.AspNetCore.Mvc.Dapr.EventBus.Controllers;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
public class TiknasAspNetCoreMvcDaprEventsController : TiknasController
{
    [HttpPost(TiknasAspNetCoreMvcDaprPubSubConsts.DaprEventCallbackUrl)]
    public virtual async Task<IActionResult> EventAsync()
    {
        HttpContext.ValidateDaprAppApiToken();

        var daprSerializer = HttpContext.RequestServices.GetRequiredService<IDaprSerializer>();
        var body = (await JsonDocument.ParseAsync(HttpContext.Request.Body));

        var pubSubName = body.RootElement.GetProperty("pubsubname").GetString();
        var topic = body.RootElement.GetProperty("topic").GetString();
        var data = body.RootElement.GetProperty("data").GetRawText();
        if (pubSubName.IsNullOrWhiteSpace() || topic.IsNullOrWhiteSpace() || data.IsNullOrWhiteSpace())
        {
            Logger.LogError("Invalid Dapr event request.");
            return BadRequest();
        }

        var distributedEventBus = HttpContext.RequestServices.GetRequiredService<DaprDistributedEventBus>();

        if (IsTiknasDaprEventData(data))
        {
            var daprEventData = daprSerializer.Deserialize(data, typeof(TiknasDaprEventData)).As<TiknasDaprEventData>();
            var eventData = daprSerializer.Deserialize(daprEventData.JsonData, distributedEventBus.GetEventType(daprEventData.Topic));
            await distributedEventBus.TriggerHandlersAsync(distributedEventBus.GetEventType(daprEventData.Topic), eventData, daprEventData.MessageId, daprEventData.CorrelationId);
        }
        else
        {
            var eventData = daprSerializer.Deserialize(data, distributedEventBus.GetEventType(topic!));
            await distributedEventBus.TriggerHandlersAsync(distributedEventBus.GetEventType(topic!), eventData);
        }

        return Ok();
    }

    protected  virtual bool IsTiknasDaprEventData(string data)
    {
        var document = JsonDocument.Parse(data);
        var objects = document.RootElement.EnumerateObject().ToList();
        return objects.Count == 5 &&
               objects.Any(x => x.Name.Equals("PubSubName", StringComparison.CurrentCultureIgnoreCase)) &&
               objects.Any(x => x.Name.Equals("Topic", StringComparison.CurrentCultureIgnoreCase)) &&
               objects.Any(x => x.Name.Equals("MessageId", StringComparison.CurrentCultureIgnoreCase)) &&
               objects.Any(x => x.Name.Equals("JsonData", StringComparison.CurrentCultureIgnoreCase)) &&
               objects.Any(x => x.Name.Equals("CorrelationId", StringComparison.CurrentCultureIgnoreCase));
    }
}
