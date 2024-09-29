namespace Panorama.Causation.Options;

[Obsolete("This is replaced by Panorama.Backing.Dead RabbitMqOptions - remove this. This project should know nothing about RabbitMQ.")]
public class RabbitMqOptions
{
    public const string SettingName = "RabbitMq";
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}