using Teatro.Shared.Common.Xtos;

namespace Panorama.Backing.Bus.Shared.Common.Xto;

public record PagedResultXto<T> : MessageXto, IPagedResultXto<T>
{
    public virtual int TotalCount { get; init; }
    
    private IReadOnlyList<T> _items;
    public IReadOnlyList<T> Items
    {
        get { return _items ??= new List<T>(); }
        init { _items = value; }
    }

    public PagedResultXto()
    {
    }

    public PagedResultXto(int totalCount, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
}