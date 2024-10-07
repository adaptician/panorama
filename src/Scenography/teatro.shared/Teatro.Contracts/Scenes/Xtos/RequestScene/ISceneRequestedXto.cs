using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.RequestScene;

public interface ISceneRequestedXto<TData> : IResultXto<TData>
where TData : IViewSceneXto
{
    
}