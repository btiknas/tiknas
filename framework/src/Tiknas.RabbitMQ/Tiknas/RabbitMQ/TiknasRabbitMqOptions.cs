namespace Tiknas.RabbitMQ;

public class TiknasRabbitMqOptions
{
    public RabbitMqConnections Connections { get; }

    public TiknasRabbitMqOptions()
    {
        Connections = new RabbitMqConnections();
    }
}
