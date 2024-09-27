namespace Panorama.Backing.Brokers;

// Bind a <QUEUE> to an <EXCHANGE> via a <ROUTING_KEY>
public class QueueBinding(string queueName, string exchangeName, string routingKey)
{
    public string QueueName { get; set; } = queueName;
    public string ExchangeName { get; set; } = exchangeName;
    public string RoutingKey { get; set; } = routingKey;
}