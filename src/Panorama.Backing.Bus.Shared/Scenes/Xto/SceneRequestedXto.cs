using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto;

[MessageUrn("Teatro.Scenes:SceneRequestedXto")]
public record SceneRequestedXto : MessageXto//, ISceneRequestedXto
{
    public ViewSceneXto Data { get; set; }
}