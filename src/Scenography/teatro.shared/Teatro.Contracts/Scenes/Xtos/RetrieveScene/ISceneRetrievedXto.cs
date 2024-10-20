using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.RetrieveScene;

public interface ISceneRetrievedXto<TData> : IResultXto<TData>
where TData : IViewSceneXto
{
    
}