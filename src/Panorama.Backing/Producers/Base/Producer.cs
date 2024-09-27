using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Shared.Messages;
using RabbitMQ.Client;

namespace Panorama.Backing.Producers.Base;

// https://www.rabbitmq.com/client-libraries/dotnet-api-guide
public abstract class Producer
{
    private string ExchangeName { get; } 
    private readonly IRabbitMqConnectionPool _connectionPool;

    protected ILogger<Producer> Logger;
    
    public Producer(
        string exchangeName,
        ILogger<Producer> logger,
        IRabbitMqConnectionPool connectionPool)
    {
        ExchangeName = exchangeName;
        _connectionPool = connectionPool;

        Logger = logger;
    }
    
    public virtual void PublishMessage<T>(T message, string routingKey)
    where T : IBrokerMessage
    {
        Logger.LogTrace($"{nameof(Producer)} is about to send a message to Exchange: {ExchangeName} " +
                        $"with Routing Key: {routingKey}");
        
        using var connection = _connectionPool.GetConnection();
        using var channel = connection.CreateModel();
        
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        IBasicProperties props = channel.CreateBasicProperties();
        props.ContentType = "text/plain";
        props.AppId = message.AppId;
        props.MessageId = message.MessageId;
        props.DeliveryMode = (byte)message.DeliveryMode;
        props.UserId = message.UserId;
        props.CorrelationId = message.CorrelationId;
        
        channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, 
            basicProperties: props, body: body);
    }
}