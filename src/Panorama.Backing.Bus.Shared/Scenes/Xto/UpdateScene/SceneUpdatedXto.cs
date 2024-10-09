using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.UpdateScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.UpdateScene;

[MessageUrn("Teatro.Scenes:SceneUpdatedXto")]
public record SceneUpdatedXto : MessageXto, ISceneUpdatedXto<ViewSceneXto>
{
    public ViewSceneXto Data { get; set; }
}