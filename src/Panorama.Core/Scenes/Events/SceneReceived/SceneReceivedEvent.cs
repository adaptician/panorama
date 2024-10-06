using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneReceived;

public class SceneReceivedEvent : AppEvent<SceneReceivedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneReceived;
}