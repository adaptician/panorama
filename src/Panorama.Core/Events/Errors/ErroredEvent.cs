using Panorama.Events.Base;

namespace Panorama.Events.Errors;

public class ErroredEvent : AppEvent<ErroredEventData>
{
    public override string MethodName => AppEventConstants.NotifyOfSceneErrored;
}