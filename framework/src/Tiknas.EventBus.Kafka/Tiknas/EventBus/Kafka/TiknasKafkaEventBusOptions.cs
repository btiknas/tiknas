namespace Tiknas.EventBus.Kafka;

public class TiknasKafkaEventBusOptions
{

    public string? ConnectionName { get; set; }

    public string TopicName { get; set; } = default!;

    public string GroupId { get; set; } = default!;
}
