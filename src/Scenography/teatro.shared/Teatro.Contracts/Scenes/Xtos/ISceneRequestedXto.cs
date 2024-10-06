using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface ISceneRequestedXto<TData> : IResultXto<TData>
where TData : IViewSceneXto
{
    
}