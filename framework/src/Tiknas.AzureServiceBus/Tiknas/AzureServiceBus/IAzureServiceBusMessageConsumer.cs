using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Tiknas.AzureServiceBus;

public interface IAzureServiceBusMessageConsumer
{
    void OnMessageReceived(Func<ServiceBusReceivedMessage, Task> callback);
}
