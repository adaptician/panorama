using Teatro.Contracts.Common.Xtos;

namespace Teatro.Contracts.Scenes.Xtos;

public interface IScenesRequestedXto : IResultXto<IPagedResultXto<IViewSceneXto>>
{
}