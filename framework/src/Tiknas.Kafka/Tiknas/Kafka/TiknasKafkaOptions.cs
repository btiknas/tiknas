using System;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Tiknas.Kafka;

public class TiknasKafkaOptions
{
    public KafkaConnections Connections { get; }

    public Action<ProducerConfig>? ConfigureProducer { get; set; }

    public Action<ConsumerConfig>? ConfigureConsumer { get; set; }

    public Action<TopicSpecification>? ConfigureTopic { get; set; }

    public TiknasKafkaOptions()
    {
        Connections = new KafkaConnections();
    }
}
