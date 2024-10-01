using Teatro.Shared.Common.Xtos;

namespace Teatro.Shared.Scenes.Xtos;

public interface IViewSceneXto : IMessageXto
{
    public string SceneCorrelationId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ScenographyCorrelationId { get; init; }
    public string SceneData { get; init; }
}