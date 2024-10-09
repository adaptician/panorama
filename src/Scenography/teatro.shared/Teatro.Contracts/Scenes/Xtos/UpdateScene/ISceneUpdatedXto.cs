using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.UpdateScene;

public interface ISceneUpdatedXto<TData> : IResultXto<TData>
    where TData : ICorrelateScene
{
    
}