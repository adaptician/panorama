using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events;

public class ScenesReceivedCarrier : EventCarrier<ScenesReceivedEvent, ScenesReceivedEventData>
{
    public ScenesReceivedCarrier(IAppEventManager appEventManager, 
        ScenesReceivedEvent @event) 
        : base(appEventManager, @event)
    {
    }
}