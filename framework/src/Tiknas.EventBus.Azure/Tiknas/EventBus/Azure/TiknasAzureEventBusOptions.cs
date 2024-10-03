namespace Tiknas.EventBus.Azure;

public class TiknasAzureEventBusOptions
{
    public string? ConnectionName { get; set; }

    public string SubscriberName { get; set; } = default!;

    public string TopicName { get; set; } = default!;

    public bool IsServiceBusDisabled { get; set; }
}
