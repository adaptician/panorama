using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Scenes;

public interface ISceneManager
{
    ScenesReceivedCarrier CreateScenesReceivedCarrier();
}