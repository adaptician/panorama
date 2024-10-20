using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneRetrieved;

public class SceneRetrievedCarrier : EventCarrier<SceneRetrievedEvent, SceneRetrievedEventData>
{
    public SceneRetrievedCarrier(IAppEventManager appEventManager, 
        SceneRetrievedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}