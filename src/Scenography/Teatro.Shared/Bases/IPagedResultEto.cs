namespace Teatro.Shared.Bases;

public interface IPagedResultEto<T>
{
    int TotalCount { get; set; }
    IReadOnlyList<T> Items { get; set; }
}