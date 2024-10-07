using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneCreated;

public class SceneCreatedEvent : AppEvent<SceneCreatedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneCreated;
}