using Microsoft.Extensions.Logging;
using Panorama.Backing.Dead.Consumers.Base;
using Panorama.Backing.Dead.Shared.Consumers;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Dead.Workers;

public class ScenesOperationConsumerWorker : ConsumerWorker<ScenesOperationEto>
{
    public ScenesOperationConsumerWorker(ILogger<ConsumerWorker<ScenesOperationEto>> logger, 
        IConsumer<ScenesOperationEto> consumer) 
        : base(logger, consumer)
    {
    }
}