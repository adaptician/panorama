using Panorama.Events;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events;

public class ScenesReceivedEvent : AppEvent<ScenesReceivedEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfScenesReceived;
}