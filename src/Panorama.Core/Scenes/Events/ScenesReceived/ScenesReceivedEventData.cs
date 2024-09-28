using Abp.Application.Services.Dto;
using Panorama.Backing.Shared.Scenes.Requests;
using Panorama.Events.Base;

namespace Panorama.Scenes.Events;

public class ScenesReceivedEventData : AppEventData
{
    public PagedResultDto<IViewScene> Data { get; set; }
}