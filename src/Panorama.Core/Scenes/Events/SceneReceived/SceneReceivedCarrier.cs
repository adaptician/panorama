using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneReceived;

public class SceneReceivedCarrier : EventCarrier<SceneReceivedEvent, SceneReceivedEventData>
{
    public SceneReceivedCarrier(IAppEventManager appEventManager, 
        SceneReceivedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}