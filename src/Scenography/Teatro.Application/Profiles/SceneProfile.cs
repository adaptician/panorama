using AutoMapper;
using Teatro.Core.Scenes;
using Teatro.Shared.Scenes.Dtos;
using Teatro.Shared.Scenes.Etos;

namespace Teatro.Application.Profiles;

public class SceneProfile : Profile
{
    public SceneProfile()
    {
        CreateMap<Scene, ViewSceneDto>();
        CreateMap<Scene, ViewSceneEto>();
        
        CreateMap<CreateSceneDto, Scene>()
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<UpdateSceneDto, Scene>()
            .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}