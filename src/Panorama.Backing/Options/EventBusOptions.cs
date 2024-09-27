namespace Panorama.Backing.Options;

public class EventBusOptions
{
    public const string SettingName = "EventBus";
    public string BusType { get; set; } 
    public RabbitMqOptions RabbitMq { get; set; }
}