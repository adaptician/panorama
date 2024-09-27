using AutoMapper;
using Panorama.Backing.Shared.Scenes.Requests;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Scenes.Dto;

namespace Panorama.Scenes;

public class SceneMappingProfile : Profile
{
    public SceneMappingProfile()
    {
        CreateMap<PagedSceneResultRequestDto, ScenesRequested>();
        CreateMap<ScenesRequested, ScenesRequestedEto>();
    }
}