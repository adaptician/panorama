namespace Panorama.Causation.Options;

public class RabbitMqOptions
{
    public const string SettingName = "RabbitMq";
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}