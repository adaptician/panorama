using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Consumers.Base;
using Panorama.Backing.Dead.Shared.Consumers;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Dead.Workers;

public class ScenesResultConsumerWorker : ConsumerWorker<ScenesResultEto>
{
    public ScenesResultConsumerWorker(ILogger<ConsumerWorker<ScenesResultEto>> logger, 
        IConsumer<ScenesResultEto> consumer) 
        : base(logger, consumer)
    {
    }
}