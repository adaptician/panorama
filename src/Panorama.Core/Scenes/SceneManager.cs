using Panorama.Events;
using Panorama.Scenes.Events.SceneCreated;
using Panorama.Scenes.Events.SceneDeleted;
using Panorama.Scenes.Events.SceneReceived;
using Panorama.Scenes.Events.ScenesReceived;
using Panorama.Scenes.Events.SceneUpdated;

namespace Panorama.Scenes;

public class SceneManager(
    IAppEventManager appEventManager
    )
: EventCarrierManager(appEventManager), ISceneManager
{

    public ScenesReceivedCarrier CreateScenesReceivedCarrier()
    {
        var @event = new ScenesReceivedEvent();
        return new ScenesReceivedCarrier(AppEventManager, @event);
    }
    
    public SceneReceivedCarrier CreateSceneReceivedCarrier()
    {
        var @event = new SceneReceivedEvent();
        return new SceneReceivedCarrier(AppEventManager, @event);
    }
    
    public SceneCreatedCarrier CreateSceneCreatedCarrier()
    {
        var @event = new SceneCreatedEvent();
        return new SceneCreatedCarrier(AppEventManager, @event);
    }
    
    public SceneUpdatedCarrier CreateSceneUpdatedCarrier()
    {
        var @event = new SceneUpdatedEvent();
        return new SceneUpdatedCarrier(AppEventManager, @event);
    }
    
    public SceneDeletedCarrier CreateSceneDeletedCarrier()
    {
        var @event = new SceneDeletedEvent();
        return new SceneDeletedCarrier(AppEventManager, @event);
    }
}