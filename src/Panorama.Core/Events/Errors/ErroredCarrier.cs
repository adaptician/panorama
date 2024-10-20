using Panorama.Events.Base;

namespace Panorama.Events.Errors;

public class ErroredCarrier : EventCarrier<ErroredEvent, ErroredEventData>
{
    public ErroredCarrier(IAppEventManager appEventManager, ErroredEvent @event) 
        : base(appEventManager, @event)
    {
    }
}