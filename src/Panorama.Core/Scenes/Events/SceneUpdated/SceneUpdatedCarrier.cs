using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneUpdated;

public class SceneUpdatedCarrier : EventCarrier<SceneUpdatedEvent, SceneUpdatedEventData>
{
    public SceneUpdatedCarrier(IAppEventManager appEventManager, SceneUpdatedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}