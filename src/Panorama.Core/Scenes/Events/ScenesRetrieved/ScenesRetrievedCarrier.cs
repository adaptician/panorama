using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.ScenesRetrieved;

public class ScenesRetrievedCarrier : EventCarrier<ScenesRetrievedEvent, ScenesRetrievedEventData>
{
    public ScenesRetrievedCarrier(IAppEventManager appEventManager, 
        ScenesRetrievedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}