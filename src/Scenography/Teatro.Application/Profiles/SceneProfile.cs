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

        #endregion
    }
}