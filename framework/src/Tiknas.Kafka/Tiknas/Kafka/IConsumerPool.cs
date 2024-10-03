using System;
using Confluent.Kafka;

namespace Tiknas.Kafka;

public interface IConsumerPool : IDisposable
{
    IConsumer<string, byte[]> Get(string groupId, string? connectionName = null);
}
