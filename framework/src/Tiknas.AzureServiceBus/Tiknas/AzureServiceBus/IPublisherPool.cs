using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Tiknas.AzureServiceBus;

public interface IPublisherPool : IAsyncDisposable
{
    Task<ServiceBusSender> GetAsync(string topicName, string? connectionName);
}
