using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.RequestScenes;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto;

[MessageUrn("Teatro.Scenes:ScenesRequestedXto")]
public record ScenesRequestedXto : MessageXto, IScenesRequestedXto<PagedResultXto<ViewSceneXto>, ViewSceneXto>
{
    public PagedResultXto<ViewSceneXto> Data { get; init; }
}