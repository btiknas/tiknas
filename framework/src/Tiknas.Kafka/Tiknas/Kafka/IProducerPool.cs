using System;
using Confluent.Kafka;

namespace Tiknas.Kafka;

public interface IProducerPool : IDisposable
{
    IProducer<string, byte[]> Get(string? connectionName = null);
}
