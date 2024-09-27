using AutoMapper;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Scenes.Dto;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

public class SceneMappingProfile : Profile
{
    public SceneMappingProfile()
    {
        CreateMap<PagedSceneResultRequestDto, ScenesRequested>();
        CreateMap<ScenesRequested, ScenesRequestedEto>();
        
        CreateMap<SceneRequested, SceneRequestedEto>();
        
        CreateMap<CreateSceneDto, CreateSceneCommanded>();
        CreateMap<CreateSceneCommanded, CreateSceneCommandedEto>();
        
        CreateMap<UpdateSceneDto, UpdateSceneCommanded>();
        CreateMap<UpdateSceneCommanded, UpdateSceneCommandedEto>();
        
        CreateMap<DeleteSceneCommanded, DeleteSceneCommandedEto>();
    }
}