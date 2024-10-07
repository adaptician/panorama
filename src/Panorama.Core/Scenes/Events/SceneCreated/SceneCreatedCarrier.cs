using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneCreated;

public class SceneCreatedCarrier : EventCarrier<SceneCreatedEvent, SceneCreatedEventData>
{
    public SceneCreatedCarrier(IAppEventManager appEventManager, SceneCreatedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}