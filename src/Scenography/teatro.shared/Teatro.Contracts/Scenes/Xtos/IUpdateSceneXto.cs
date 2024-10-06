using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IUpdateSceneXto : IMessageXto, ICorrelateScene
{
    string Name { get; init; }

    string? Description { get; init; }
}