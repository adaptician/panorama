using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.CreateScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;

[MessageUrn("Teatro.Scenes:SceneCreatedXto")]
public record SceneCreatedXto : MessageXto, ISceneCreatedXto<ViewSceneXto>
{
    public ViewSceneXto Data { get; set; }
}