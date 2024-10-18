using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneErrored;

public class SceneErroredEvent : AppEvent<SceneErroredEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneErrored;
}