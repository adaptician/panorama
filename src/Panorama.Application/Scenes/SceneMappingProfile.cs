using AutoMapper;
using Panorama.Backing.Shared.Scenes.Requests;
using Panorama.Backing.Shared.Scenes.Requests.Eto;

namespace Panorama.Scenes;

public class SceneMappingProfile : Profile
{
    public SceneMappingProfile()
    {
        CreateMap<ScenesRequested, ScenesRequestedEto>();
    }
}