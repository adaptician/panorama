using AutoMapper;
using Panorama.Backing.Shared.Common;
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
        
        // TODO: move to common profile
        CreateMap<OperationEto, ResultEto>()
            .ForMember(dest => dest.Data, opt => opt.Ignore());
    }
}