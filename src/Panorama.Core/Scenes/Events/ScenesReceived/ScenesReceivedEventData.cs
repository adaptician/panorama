using Abp.Application.Services.Dto;
using Panorama.Events.Base;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes.Events.ScenesReceived;

public class ScenesReceivedEventData : AppEventData
{
    public PagedResultDto<ViewSceneDto> Data { get; set; }
}