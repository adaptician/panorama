using Abp.Application.Services.Dto;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Dto;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events.ScenesReceived;

public class ScenesReceivedEventData : AppEventData
{
    public PagedResultDto<ViewSceneDto> Data { get; set; }
}