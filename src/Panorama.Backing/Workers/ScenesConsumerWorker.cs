using Microsoft.Extensions.Logging;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Consumers;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Workers;

public class ScenesConsumerWorker : ConsumerWorker<ScenesRequestedEto>
{
    public ScenesConsumerWorker(ILogger<ConsumerWorker<ScenesRequestedEto>> logger, 
        IConsumer<ScenesRequestedEto> consumer) 
        : base(logger, consumer)
    {
    }
}