using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Brokers;
using Panorama.Backing.Dead.ConnectionPools;
using Panorama.Backing.Dead.Consumers.Base;
using Panorama.Backing.Dead.Shared.Messages;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Dead.Consumers;

public class ScenesOperationConsumer : Consumer<ScenesOperationEto>
{
    private static Queue Queue = Queues.DefinedQueues.ScenesOperation;

    public ScenesOperationConsumer(IRabbitMqConnectionPool connectionPool, 
        IProcessMessageHandler<ScenesOperationEto> handler,
        ILogger<ScenesOperationConsumer> logger) 
        : base(Queue, connectionPool, handler, logger)
    {
    }
}