using MassTransit;

namespace Panorama.Backing.Bus.Scenes.ScenesRequested;

public class ScenesRequestedConsumerDefinition : ConsumerDefinition<ScenesRequestedConsumer>
{
    public ScenesRequestedConsumerDefinition()
    {
        // EndpointName = "Teatro.Scenes:RequestScenesXto";
    }
}