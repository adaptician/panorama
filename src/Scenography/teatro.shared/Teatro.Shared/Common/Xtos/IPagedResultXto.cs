namespace Teatro.Shared.Common.Xtos;

public interface IPagedResultXto<T> : IMessageXto
{
    int TotalCount { get; init; }
    
    IReadOnlyList<T> Items { get; init; }
}