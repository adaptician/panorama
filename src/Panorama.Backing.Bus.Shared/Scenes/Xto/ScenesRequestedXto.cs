using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Shared.Common.Xtos;
using Teatro.Shared.Scenes.Xtos;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto;

[MessageUrn("Teatro.Scenes:ScenesRequestedXto")]
public record ScenesRequestedXto : MessageXto//, IScenesRequestedXto
{
    public PagedResultXto<ViewSceneXto> Data { get; init; }
}