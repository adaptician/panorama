using Panorama.Events;
using Panorama.Scenes.Events.SceneCreated;
using Panorama.Scenes.Events.SceneDeleted;
using Panorama.Scenes.Events.SceneRetrieved;
using Panorama.Scenes.Events.ScenesRetrieved;
using Panorama.Scenes.Events.SceneUpdated;

namespace Panorama.Scenes;

public class SceneManager(
    IAppEventManager appEventManager
    )
: EventCarrierManager(appEventManager), ISceneManager
{

    public ScenesRetrievedCarrier CreateScenesReceivedCarrier()
    {
        var @event = new ScenesRetrievedEvent();
        return new ScenesRetrievedCarrier(AppEventManager, @event);
    }
    
    public SceneRetrievedCarrier CreateSceneReceivedCarrier()
    {
        var @event = new SceneRetrievedEvent();
        return new SceneRetrievedCarrier(AppEventManager, @event);
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