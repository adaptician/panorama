using Abp.Application.Services.Dto;
using Panorama.Backing.Dead.Shared.Scenes.Requests;

namespace Panorama.Scenes.Dto;

public class PagedSceneResultRequestDto : PagedResultRequestDto, IRequestScenes
{
    public string Keyword { get; set; }
}