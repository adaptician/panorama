using Microsoft.Extensions.Options;
using Panorama.Backing.Options;
using RabbitMQ.Client;

namespace Panorama.Backing.ConnectionPools;

public class RabbitMqConnectionPool : IRabbitMqConnectionPool, IDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IModel? _channel;
    
    public RabbitMqConnectionPool(IOptions<EventBusOptions> eventBusOptions)
    {
        var options = eventBusOptions.Value;
        
        var rabbitMqOptions = options.RabbitMq ?? throw new Exception("Unable to provision RabbitMQ - " +
                                                                      "configurations are missing.");
        _factory = new ConnectionFactory
        {
            HostName = rabbitMqOptions.HostName,
            UserName = rabbitMqOptions.UserName,
            Password = rabbitMqOptions.Password
        };
    }

    public IConnection GetConnection()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = _factory.CreateConnection();
        }
        
        return _connection;
    }

    public IModel GetChannel()
    {
        var connection = GetConnection();
        if (_channel == null || !_channel.IsOpen)
        {
            _channel = connection.CreateModel();
        }

        return _channel;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}