using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IScenesRequestedXto<TPage, TData> : IResultXto<TPage>
where TPage : IPagedResultXto<TData>
where TData : IViewSceneXto
{
}