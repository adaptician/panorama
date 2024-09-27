using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Panorama.Backing.Brokers;
using Panorama.Backing.Options;
using RabbitMQ.Client;

namespace Panorama.Backing.Provisioners;

public class RabbitMqProvisioner : IHostedService
{
    private readonly ILogger<RabbitMqProvisioner> _logger;
    private readonly IModel _channel;

    public RabbitMqProvisioner(IOptions<EventBusOptions> eventBusOptions,
        ILogger<RabbitMqProvisioner> logger)
    {
        _logger = logger;
        var options = eventBusOptions.Value;

        var rabbitMqOptions = options.RabbitMq ?? throw new Exception("Unable to provision RabbitMQ - " +
                                                                             "configurations are missing.");

        var factory = new ConnectionFactory
        {
            HostName = rabbitMqOptions.HostName,
            UserName = rabbitMqOptions.UserName,
            Password = rabbitMqOptions.Password
        };
        
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Provisioning exchanges ...");
        foreach (var exchange in Exchanges.GetAll())
        {
            _logger.LogTrace($"Provision exchange: {exchange.Key} of type: {exchange.Value}");
            _channel.ExchangeDeclare(exchange.Key, exchange.Value);
        }

        foreach (var queue in Queues.GetAll())
        {
            _logger.LogTrace($"Provision queue: {queue.Name}");
            _channel.QueueDeclare(queue.Name, queue.IsDurable, queue.IsExclusive, queue.WillAutoDelete);
        }

        foreach (var queueBinding in QueueBindings.GetAll())
        {
            _logger.LogTrace($"Provision binding of Queue: {queueBinding.QueueName} " +
                             $"to Exchange: {queueBinding.ExchangeName} " +
                             $"via Routing Key: {queueBinding.RoutingKey}");
            _channel.QueueBind(queueBinding.QueueName, queueBinding.ExchangeName, queueBinding.RoutingKey);
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        return Task.CompletedTask;
    }
}











