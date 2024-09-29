using Abp.Domain.Services;
using Panorama.Events;
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
}