using AutoMapper;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto;

namespace Panorama.Scenes;

public class SceneMappingProfile : Profile
{
    public SceneMappingProfile()
    {
        CreateMap<ViewSceneXto, ViewSceneDto>();
    }
}