using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RequestScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScene;

[MessageUrn(SceneMessageUrn.TeatroScenes_SceneRequested)]
public record SceneRequestedXto : MessageXto, ISceneRequestedXto<ViewSceneXto>
{
    public ViewSceneXto Data { get; set; }
}