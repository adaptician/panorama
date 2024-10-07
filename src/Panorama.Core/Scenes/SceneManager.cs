using Abp.Domain.Services;
using Panorama.Events;
using Panorama.Scenes.Events.SceneCreated;
using Panorama.Scenes.Events.SceneReceived;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Scenes;

public class SceneManager(
    IAppEventManager appEventManager
    )
: DomainService, ISceneManager
{
    public ScenesReceivedCarrier CreateScenesReceivedCarrier()
    {
        var @event = new ScenesReceivedEvent();
        return new ScenesReceivedCarrier(appEventManager, @event);
    }
    
    public SceneReceivedCarrier CreateSceneReceivedCarrier()
    {
        var @event = new SceneReceivedEvent();
        return new SceneReceivedCarrier(appEventManager, @event);
    }
    
    public SceneCreatedCarrier CreateSceneCreatedCarrier()
    {
        var @event = new SceneCreatedEvent();
        return new SceneCreatedCarrier(appEventManager, @event);
    }
}