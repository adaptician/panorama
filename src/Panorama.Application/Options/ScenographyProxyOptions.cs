namespace Panorama.Options;

public class ScenographyProxyOptions
{
    public const string SettingName = "ScenographyProxy";
    
    /// <summary>
    /// The base URL for the web API.
    /// </summary>
    public string BaseUrl { get; set; }
    
    /// <summary>
    /// The authorization key used to validate the call to the proxy.
    /// </summary>
    public string SecretKey { get; set; }
}