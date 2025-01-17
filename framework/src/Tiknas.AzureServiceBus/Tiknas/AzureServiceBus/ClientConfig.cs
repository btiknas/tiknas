using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace Tiknas.AzureServiceBus;

public class ClientConfig
{
    public string ConnectionString { get; set; } = default!;

    public ServiceBusAdministrationClientOptions Admin { get; set; } = new();

    public ServiceBusClientOptions Client { get; set; } = new();

    public ServiceBusProcessorOptions Processor { get; set; } = new();
}
