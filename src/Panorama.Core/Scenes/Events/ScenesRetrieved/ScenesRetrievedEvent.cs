using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.ScenesRetrieved;

public class ScenesRetrievedEvent : AppEvent<ScenesRetrievedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfScenesRetrieved;
}