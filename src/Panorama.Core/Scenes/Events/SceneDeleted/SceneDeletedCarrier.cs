using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneDeleted;

public class SceneDeletedCarrier : EventCarrier<SceneDeletedEvent, SceneDeletedEventData>
{
    public SceneDeletedCarrier(IAppEventManager appEventManager, SceneDeletedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}