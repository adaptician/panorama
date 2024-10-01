using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IViewSceneXto : IMessageXto
{
    string SceneCorrelationId { get; init; }
    string Name { get; init; }
    string Description { get; init; }
    string ScenographyCorrelationId { get; init; }
    string SceneData { get; init; }
}