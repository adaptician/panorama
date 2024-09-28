using Microsoft.Extensions.Logging;
using Panorama.Backing.Consumers.Base;
using Panorama.Backing.Shared.Consumers;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Backing.Workers;

public class ScenesOperationConsumerWorker : ConsumerWorker<ScenesOperationEto>
{
    public ScenesOperationConsumerWorker(ILogger<ConsumerWorker<ScenesOperationEto>> logger, 
        IConsumer<ScenesOperationEto> consumer) 
        : base(logger, consumer)
    {
    }
}