using AutoMapper;
using Teatro.Core.Scenes;
using Teatro.Shared.Scenes.Dtos;

namespace Teatro.Application.Profiles;

public class SceneProfile : Profile
{
    public SceneProfile()
    {
        #region Scene

        CreateMap<Scene, ViewSceneDto>();
        
        CreateMap<CreateSceneDto, Scene>()
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<UpdateSceneDto, Scene>()
            .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        #endregion
    }
}