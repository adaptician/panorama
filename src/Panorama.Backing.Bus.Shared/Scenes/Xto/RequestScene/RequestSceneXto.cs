using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.RequestScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScene;

[MessageUrn("Teatro.Scenes:RequestSceneXto")]
public record RequestSceneXto : MessageXto, IRequestSceneXto
{
    public string SceneCorrelationId { get; init; }
}