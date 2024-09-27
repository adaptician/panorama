using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Panorama.Backing.Options;
using Panorama.Common.Enums;
using Panorama.Common.Extensions;
using RabbitMQ.Client;

namespace Panorama.Backing.Producers.Base;

// https://www.rabbitmq.com/client-libraries/dotnet-api-guide
public abstract class Producer
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    
    private string _exchangeName { get; set; }
    private string _exchangeType { get; set; }
    private string _queueName { get; set; }
    private string _route { get; set; }
    
    public Producer(
        EventBusExchangeTypeEnum exchangeType,
        string exchangeName,
        string queueName,
        string route,
        ILogger<Producer> logger,
        IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;

        _exchangeType = exchangeType.GetCode();
        _exchangeName = exchangeName;
        _queueName = queueName;
        _route = route;
    }
    
    public void SendMessage<T>(T message)
    {
        // var factory = new ConnectionFactory
        // {
        //     HostName = _rabbitMqOptions.HostName,
        //     UserName = _rabbitMqOptions.UserName,
        //     Password = _rabbitMqOptions.Password
        // };
        //
        // var connection = factory.CreateConnection();
        //
        // using
        //     var channel = connection.CreateModel();
        //
        // channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        // channel.QueueDeclare(queueName, exclusive: false);
        // // eg             queueName          exchangeName     routingKey   
        // // eg             request_getAll     scenes           getAll   
        // // eg             request_get        scenes           get 
        // // eg             request_create     scenes           create 
        // // eg             request_update     scenes           update 
        // // eg             request_delete     scenes           delete 
        // channel.QueueBind(queueName, exchangeName, routingKey, null);
        //
        // var json = JsonConvert.SerializeObject(message);
        // var body = Encoding.UTF8.GetBytes(json);
        //
        // IBasicProperties props = channel.CreateBasicProperties();
        // props.ContentType = "text/plain";
        // props.DeliveryMode = 2;
        //
        // channel.BasicPublish(exchange: "", routingKey: _route, basicProperties: props, body: body);
    }
}