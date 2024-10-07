using Newtonsoft.Json;

namespace Panorama.Backing.Bus.Shared.Scenes.Dto;

public class CreateSceneDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("sceneData")]
    public string SceneData { get; set; }
}