namespace Tiknas.EventBus.Dapr;

public class TiknasDaprEventBusOptions
{
    public string PubSubName { get; set; }

    public TiknasDaprEventBusOptions()
    {
        PubSubName = "pubsub";
    }
}
