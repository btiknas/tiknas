using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Tiknas.Kafka;

public interface IKafkaMessageConsumer
{
    void OnMessageReceived(Func<Message<string, byte[]>, Task> callback);
}
