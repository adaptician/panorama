using Microsoft.Extensions.Logging;
using Panorama.Backing.Brokers;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Consumers;

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