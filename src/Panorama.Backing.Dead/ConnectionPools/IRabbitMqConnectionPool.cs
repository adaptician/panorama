using RabbitMQ.Client;

namespace Panorama.Backing.Dead.ConnectionPools;

public interface IRabbitMqConnectionPool
{
    IConnection GetConnection();
}