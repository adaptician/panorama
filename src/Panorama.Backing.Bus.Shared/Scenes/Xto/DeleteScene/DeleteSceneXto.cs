using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.DeleteScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.DeleteScene;

[MessageUrn(SceneMessageUrn.TeatroScenes_DeleteScene)]
public record DeleteSceneXto : MessageXto, IDeleteSceneXto
{
    public string SceneCorrelationId { get; init; }
}