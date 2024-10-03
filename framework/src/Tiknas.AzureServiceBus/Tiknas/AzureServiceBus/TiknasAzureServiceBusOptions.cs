namespace Tiknas.AzureServiceBus;

public class TiknasAzureServiceBusOptions
{
    public AzureServiceBusConnections Connections { get; }

    public TiknasAzureServiceBusOptions()
    {
        Connections = new AzureServiceBusConnections();
    }
}
