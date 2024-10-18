using Abp.Domain.Services;
using Panorama.Events.Errors;

namespace Panorama.Events;

public abstract class EventCarrierManager(IAppEventManager appEventManager)
    : DomainService
{
    protected readonly IAppEventManager AppEventManager = appEventManager;

    public ErroredCarrier CreateErroredCarrier()
    {
        var @event = new ErroredEvent();
        return new ErroredCarrier(AppEventManager, @event);
    }
}