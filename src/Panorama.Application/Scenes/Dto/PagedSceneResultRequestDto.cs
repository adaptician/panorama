using Abp.Application.Services.Dto;

namespace Panorama.Scenes.Dto;

// TODO: where is the interface?
public class PagedSceneResultRequestDto : PagedResultRequestDto//, IRequestScenes
{
    public string Keyword { get; set; }
}