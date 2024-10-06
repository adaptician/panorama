using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Common.Dto;

namespace Panorama.Backing.Bus.Shared.Scenes.Dto;

[Serializable]
// TODO: do not expose db id's
public class ViewSceneDto : EntityDto<long>
{
    [JsonProperty("correlationId")]
    public string CorrelationId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("scenographyId")]
    // TODO: do not expose db id's
    public long ScenographyId { get; set; }
    
    [JsonProperty("sceneData")]
    public string SceneData { get; set; }
}