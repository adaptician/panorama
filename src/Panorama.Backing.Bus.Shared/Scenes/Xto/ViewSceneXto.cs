using Panorama.Backing.Bus.Shared.Common.Xto;
using Teatro.Shared.Scenes.Xtos;

namespace Panorama.Backing.Bus.Shared.Scenes.Xto;

public record ViewSceneXto : MessageXto, IViewSceneXto
{
    public string SceneCorrelationId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ScenographyCorrelationId { get; init; }
    public string SceneData { get; init; }
}