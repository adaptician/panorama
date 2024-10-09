using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.UpdateScene;

public interface IUpdateSceneXto : IMessageXto, ICorrelateScene
{
    string Name { get; }

    string? Description { get; }
}