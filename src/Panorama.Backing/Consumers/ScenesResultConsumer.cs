using Microsoft.Extensions.Logging;
using Panorama.Backing.Brokers;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Consumers;

public class ScenesResultConsumer : Consumer<ScenesResultEto>
{
    private static Queue Queue = Queues.DefinedQueues.ScenesResult;
    
    public ScenesResultConsumer( 
        IRabbitMqConnectionPool connectionPool, 
        IProcessMessageHandler<ScenesResultEto> processMessageHandler, 
        ILogger<Consumer<ScenesResultEto>> logger) 
        : base(Queue, connectionPool, processMessageHandler, logger)
    {
    }
}