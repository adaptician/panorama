using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneDeleted;

public class SceneDeletedEvent : AppEvent<SceneDeletedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneDeleted;
}