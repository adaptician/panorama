using RabbitMQ.Client;

namespace Panorama.Backing.ConnectionPools;

public interface IRabbitMqConnectionPool
{
    IConnection GetConnection();
}