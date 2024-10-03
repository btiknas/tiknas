using System;
using RabbitMQ.Client;

namespace Tiknas.RabbitMQ;

public interface IConnectionPool : IDisposable
{
    IConnection Get(string? connectionName = null);
}
