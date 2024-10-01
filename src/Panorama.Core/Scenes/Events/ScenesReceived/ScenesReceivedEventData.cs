using Newtonsoft.Json;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.ScenesReceived;

public class ScenesReceivedEventData : AppEventData
{
    [JsonProperty("data")]
    public PagedResultDto<ViewSceneDto> Data { get; set; }
}