using Microsoft.Extensions.Logging;
using Panorama.Backing.Brokers;
using Panorama.Backing.ConnectionPools;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Consumers;

// TODO: this will need to be renamed
// Try switching this to a topic instead. Anything to do with SceneQuery
public class ScenesConsumer : Consumer<ScenesRequestedEto>
{
    private static Queue Queue = Queues.DefinedQueues.QueryScenesGetAll;

    public ScenesConsumer(IRabbitMqConnectionPool connectionPool, 
        IProcessMessageHandler<ScenesRequestedEto> handler,
        ILogger<ScenesConsumer> logger) 
        : base(Queue, connectionPool, handler, logger)
    {
    }
}