using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneUpdated;

public class SceneUpdatedEvent : AppEvent<SceneUpdatedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneUpdated;
}