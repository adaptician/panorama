using MassTransit;

namespace Panorama.Backing.Bus.Scenes;

public class ScenesConsumerDefinition : ConsumerDefinition<ScenesConsumer>
{
    public ScenesConsumerDefinition()
    {
        // EndpointName = "Teatro.Scenes:RequestScenesXto";
    }
}