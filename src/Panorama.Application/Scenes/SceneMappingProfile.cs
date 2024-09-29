using AutoMapper;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Eto;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Mediations;
using Panorama.Scenes.Dto;
using Panorama.Scenes.Events.ScenesReceived;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

public class SceneMappingProfile : Profile
{
    public SceneMappingProfile()
    {
        // TODO: clean up
        CreateMap<PagedSceneResultRequestDto, ScenesRequested>();
        
        CreateMap<ScenesOperation, ScenesOperationEto>();
        
        
        
        
        
        
        
        
        CreateMap<SceneRequested, SceneRequestedEto>();
        
        CreateMap<CreateSceneDto, CreateSceneCommanded>();
        CreateMap<CreateSceneCommanded, CreateSceneCommandedEto>();
        
        CreateMap<UpdateSceneDto, UpdateSceneCommanded>();
        CreateMap<UpdateSceneCommanded, UpdateSceneCommandedEto>();
        
        CreateMap<DeleteSceneCommanded, DeleteSceneCommandedEto>();
    }
}