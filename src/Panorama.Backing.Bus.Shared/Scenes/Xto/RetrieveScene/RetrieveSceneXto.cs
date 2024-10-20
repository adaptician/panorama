using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RetrieveScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScene;

[MessageUrn(SceneMessageUrn.TeatroScenes_RetrieveScene)]
public record RetrieveSceneXto : MessageXto, IRetrieveSceneXto
{
    public string SceneCorrelationId { get; init; }
}