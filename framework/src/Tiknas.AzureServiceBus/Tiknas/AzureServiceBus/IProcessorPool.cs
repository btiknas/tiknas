using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Tiknas.AzureServiceBus;

public interface IProcessorPool : IAsyncDisposable
{
    Task<ServiceBusProcessor> GetAsync(string subscriptionName, string topicName, string connectionName);
}
