using Microsoft.Extensions.Logging;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Consumers;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Workers;

public class ScenesResultConsumerWorker : ConsumerWorker<ScenesResultEto>
{
    public ScenesResultConsumerWorker(ILogger<ConsumerWorker<ScenesResultEto>> logger, 
        IConsumer<ScenesResultEto> consumer) 
        : base(logger, consumer)
    {
    }
}