using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts;
using Teatro.Contracts.Scenes.Xtos.RequestScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScenes;

[MessageUrn(SceneMessageUrn.TeatroScenes_ScenesRequested)]
public record ScenesRequestedXto : MessageXto, IScenesRequestedXto<PagedResultXto<ViewSceneXto>, ViewSceneXto>
{
    public PagedResultXto<ViewSceneXto> Data { get; init; }
}