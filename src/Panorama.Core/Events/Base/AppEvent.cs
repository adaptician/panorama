namespace Panorama.Events.Base;

public abstract class AppEvent<TAppEventData>
    where TAppEventData : AppEventData, new()
{
    public abstract string MethodName { get; }

    public TAppEventData Data { get; set; }
}