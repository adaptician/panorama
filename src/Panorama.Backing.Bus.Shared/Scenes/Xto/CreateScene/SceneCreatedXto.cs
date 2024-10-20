using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.CreateScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;

[MessageUrn(SceneMessageUrn.TeatroScenes_SceneCreated)]
public record SceneCreatedXto : MessageXto, ISceneCreatedXto<ViewSceneXto>
{
    public ViewSceneXto? Data { get; set; }
}