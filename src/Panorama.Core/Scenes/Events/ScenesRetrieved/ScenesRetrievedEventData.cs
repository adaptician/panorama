using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.ScenesRetrieved;

public class ScenesRetrievedEventData : AppEventData
{
    [JsonProperty("data")]
    public PagedResultDto<ViewSceneDto> Data { get; set; }
}