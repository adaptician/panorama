using System.Threading.Tasks;
using Abp;

namespace Panorama.Events.Base;

public abstract class EventCarrier<TEvent, TEventData>
    where TEvent : AppEvent<TEventData>
    where TEventData : AppEventData, new()
{
    private readonly IAppEventManager _appEventManager;
    private readonly TEvent _event;
    
    protected EventCarrier(IAppEventManager appEventManager, 
        TEvent @event)
    {
        _appEventManager = appEventManager;
        _event = @event;
    }
    
    public async Task Broadcast(TEventData progressEventData,
        UserIdentifier receiver = null)
    {
        _event.Data = progressEventData;
        if (receiver == null)
        {
            // Send to all client devices e.g. backing service has triggered broadcast.
            await _appEventManager.SendEventAsync(_event);    
        }
        else
        {
            // Send to the receivers client devices only.
            await _appEventManager.SendEventAsync(_event, receiver);
        }
    }
}