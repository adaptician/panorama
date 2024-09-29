using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Brokers;
using Panorama.Backing.Dead.ConnectionPools;
using Panorama.Backing.Dead.Shared.Consumers;
using Panorama.Backing.Dead.Shared.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Panorama.Backing.Dead.Consumers.Base;

public abstract class Consumer<TPayload> : DefaultBasicConsumer, IConsumer<TPayload>
{
    private Queue Queue { get; }
    private readonly IModel _channel;
    private readonly IProcessMessageHandler<TPayload> _processMessageHandler;
    
    protected ILogger<Consumer<TPayload>> Logger;

    public Consumer(Queue queue, IRabbitMqConnectionPool connectionPool,
        IProcessMessageHandler<TPayload> processMessageHandler,
        ILogger<Consumer<TPayload>> logger)
    {
        Queue = queue;
        var connection = connectionPool.GetConnection();
        _channel = connection.CreateModel();
        _processMessageHandler = processMessageHandler;

        Logger = logger;
    }
    
    public void StartConsuming()
    {
        // Ensure the queue exists.
        _channel.QueueDeclare(queue: Queue.Name,
            durable: Queue.IsDurable,
            exclusive: Queue.IsExclusive,
            autoDelete: Queue.WillAutoDelete);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                await ProcessMessage(message);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error processing message for Queue: {Queue.Name} - {ex.Message}");
                _channel.BasicReject(ea.DeliveryTag, true);
            }

            // Acknowledge message if successfully processed.
            _channel.BasicAck(ea.DeliveryTag, false);
            await Task.Yield();
        };

        _channel.BasicConsume(queue: Queue.Name,
            autoAck: false, // Manual acknowledgment for reliability.
            consumer: consumer);
        
        Logger.LogTrace($"Consumer started, listening to Queue: {Queue.Name}");
    }

    private async Task ProcessMessage(string message)
    {
        var payload = JsonSerializer.Deserialize<TPayload>(message);
        
        if (payload != null)
        {
            await _processMessageHandler.ProcessMessageAsync(payload);
        }
    }

    public void StopConsuming()
    {
        _channel.Close();
    }
    
    public void Dispose()
    {
        _channel.Close();
    }
}