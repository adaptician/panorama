using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneErrored;

public class SceneErroredEventData : AppEventData
{
    [JsonProperty("error")]
    public ErrorDto Error { get; set; }
}