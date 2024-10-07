using MassTransit;
using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Contracts.Scenes.Xtos.CreateScene;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;

[MessageUrn("Teatro.Scenes:CreateSceneXto")]
public record CreateSceneXto : MessageXto, ICreateSceneXto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string SceneData { get; init; }
}