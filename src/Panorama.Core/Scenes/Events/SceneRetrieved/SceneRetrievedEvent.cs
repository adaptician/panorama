using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneRetrieved;

public class SceneRetrievedEvent : AppEvent<SceneRetrievedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneRetrieved;
}