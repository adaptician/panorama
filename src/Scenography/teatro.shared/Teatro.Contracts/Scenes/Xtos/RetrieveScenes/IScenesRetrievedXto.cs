using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos.RetrieveScenes;

public interface IScenesRetrievedXto<TPage, TData> : IResultXto<TPage>
where TPage : IPagedResultXto<TData>
where TData : IViewSceneXto
{
}