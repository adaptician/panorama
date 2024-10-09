using Newtonsoft.Json;

namespace Panorama.Backing.Bus.Shared.Scenes.Dto;

public class UpdateSceneDto
{
    [JsonProperty("correlationId")]
    public string CorrelationId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
}