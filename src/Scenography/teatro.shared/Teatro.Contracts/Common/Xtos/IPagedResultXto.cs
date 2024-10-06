namespace Teatro.Contracts.Common.Xtos;

public interface IPagedResultXto<T> : IMessageXto
{
    int TotalCount { get; }
    
    IReadOnlyList<T> Items { get; }
}