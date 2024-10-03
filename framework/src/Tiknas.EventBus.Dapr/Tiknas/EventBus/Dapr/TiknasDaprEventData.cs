namespace Tiknas.EventBus.Dapr;

public class TiknasDaprEventData
{
    public string PubSubName { get; set; }

    public string Topic { get; set; }

    public string MessageId { get; set; }

    public string JsonData { get; set; }

    public string? CorrelationId { get; set; }

    public TiknasDaprEventData(string pubSubName, string topic, string messageId, string jsonData, string? correlationId)
    {
        PubSubName = pubSubName;
        Topic = topic;
        MessageId = messageId;
        JsonData = jsonData;
        CorrelationId = correlationId;
    }
}
