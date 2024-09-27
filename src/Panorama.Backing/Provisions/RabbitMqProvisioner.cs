using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Panorama.Backing.Options;
using Panorama.Common.Enums;
using Panorama.Common.Extensions;
using RabbitMQ.Client;

namespace Panorama.Backing.Provisions;

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
        foreach (var exchange in AllExchanges.GetAllExchanges())
        {
            _logger.LogTrace($"Provision exchange: {exchange.Key} of type: {exchange.Value}");
            _channel.ExchangeDeclare(exchange.Key, exchange.Value);
        }

        foreach (var queue in AllQueues.GetAllQueues())
        {
            _logger.LogTrace($"Provision queue: {queue}");
            _channel.QueueDeclare(queue.Name, queue.IsDurable, queue.IsExclusive, queue.WillAutoDelete);
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        return Task.CompletedTask;
    }
}

public class RabbitMqExchange
{
    public EventBusExchangeTypeEnum ExchangeType { get; set; }
    public string ExchangeName { get; set; }
}

public class RabbitMqBinding
{
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }
    public string RoutingKey { get; set; }
}

public class RabbitMqQueue(string name, bool isDurable = false, bool isExclusive = false, bool willAutoDelete = true)
{
    public string Name { get; set; } = name;
    
    // Transient queues will be deleted on node boot.
    // They therefore will not survive a node restart, by design.
    // Messages in transient queues will also be discarded.
    public bool IsDurable { get; set; } = isDurable;
    
    // An exclusive queue can only be used (consumed from, purged, deleted, etc) by its declaring connection.
    // In RabbitMQ a queue is exclusive by default, but
    // auto-provisioned queues should not be marked as exclusive.
    public bool IsExclusive { get; set; } = isExclusive;
    
    public bool WillAutoDelete { get; set; } = willAutoDelete;
}



public abstract class AllQueues : ReflectToList<RabbitMqQueue>
{
    public static List<RabbitMqQueue> GetAllQueues()
    {
        return GetAll([typeof(DefinedQueues)]);
    }
}

public static class DefinedQueues
{
    public static RabbitMqQueue ScenesGetAll = new (QueueNames.ScenesGetAll);
    public static RabbitMqQueue ScenesGet = new (QueueNames.ScenesGet);
    public static RabbitMqQueue SceneCreate = new (QueueNames.SceneCreate);
    public static RabbitMqQueue SceneUpdate = new (QueueNames.SceneUpdate);
    public static RabbitMqQueue SceneDelete = new (QueueNames.SceneDelete);
}

public static class QueueNames
{
    public const string ScenesGetAll = "scenes_getAll";
    public const string ScenesGet = "scenes_get";
    public const string SceneCreate = "scene_create";
    public const string SceneUpdate = "scene_update";
    public const string SceneDelete = "scene_delete";
}

public abstract class AllExchanges : ReflectToDictionary<string, string>
{
    public static Dictionary<string, string> GetAllExchanges()
    {
        return GetAll([typeof(DefinedExchanges)]);
    }
}

public class ReflectToList<TItem>
{
    protected static List<TItem> GetAll(Type[] staticTypes)
    {
        List<FieldInfo> fields = new List<FieldInfo>();
            
        foreach (var staticType in staticTypes)
        {
            if (staticType != null)
            {
                fields.AddRange(staticType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());
            }
        }
            
        List<TItem> items = new List<TItem>();
            
        foreach (FieldInfo field in fields)
        {
            // Check if the field is literal (constant) and static
            if ((field.IsLiteral || field.IsStatic) && !field.IsInitOnly)
            {
                // Add the constant value to the list
                items.Add((TItem)(field.GetValue(null)
                          ?? throw new NullReferenceException("Unable to resolve reflection - value is null.")));
            }
        }

        return items;
    }
}

public class ReflectToDictionary<TKey, TValue> 
    where TKey : notnull
{
    protected static Dictionary<TKey, TValue> GetAll(Type[] staticTypes)
    {
        List<FieldInfo> fields = new List<FieldInfo>();
        
        foreach (var staticType in staticTypes)
        {
            if (staticType != null)
            {
                fields.AddRange(staticType.GetFields(BindingFlags.Public | BindingFlags.Static).ToList());
            }
        }
            
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            
        foreach (FieldInfo field in fields)
        {
            // Check if the field is a tuple.
            if (field.FieldType == typeof((TKey, TValue)))
            {
                var tupleValue = (ValueTuple<TKey, TValue>)(field.GetValue(null) 
                    ?? throw new NullReferenceException("Unable to resolve reflection - value is null."));

                dictionary.Add(tupleValue.Item1, tupleValue.Item2);
            }
        }
        
        return dictionary;
    }
}

public static class DefinedExchanges
{
    public static (string, string) ScenesExchange = (ExchangeNames.ScenesExchange, EventBusExchangeTypeEnum.Direct.GetCode());
}

public static class ExchangeNames
{
    public const string ScenesExchange = "scenes";
}