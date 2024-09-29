using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Brokers;
using Panorama.Backing.Dead.ConnectionPools;
using Panorama.Backing.Dead.Consumers.Base;
using Panorama.Backing.Dead.Shared.Messages;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Dead.Consumers;

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