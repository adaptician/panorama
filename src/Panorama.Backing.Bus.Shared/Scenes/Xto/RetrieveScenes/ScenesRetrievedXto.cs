using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RetrieveScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScenes;

[MessageUrn(SceneMessageUrn.TeatroScenes_ScenesRetrieved)]
public record ScenesRetrievedXto : MessageXto, IScenesRetrievedXto<PagedResultXto<ViewSceneXto>, ViewSceneXto>
{
    public PagedResultXto<ViewSceneXto>? Data { get; init; }
}