using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneErrored;

public class SceneErroredCarrier : EventCarrier<SceneErroredEvent, SceneErroredEventData>
{
    public SceneErroredCarrier(IAppEventManager appEventManager, SceneErroredEvent @event) 
        : base(appEventManager, @event)
    {
    }
}