using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.SceneRetrieved;

public class SceneRetrievedEventData : AppEventData
{
    [JsonProperty("data")]
    public ViewSceneDto Data { get; set; }
}