using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.CreateScene;

public interface ISceneCreatedXto<TData> : IResultXto<TData>
where TData : ICorrelateScene
{
}