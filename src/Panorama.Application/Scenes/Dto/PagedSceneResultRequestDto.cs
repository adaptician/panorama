using Abp.Application.Services.Dto;

namespace Panorama.Scenes.Dto;

public class PagedSceneResultRequestDto : PagedResultRequestDto
{
    public string Keyword { get; set; }
}