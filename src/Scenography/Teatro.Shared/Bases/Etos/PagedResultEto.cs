using Panorama.Backing.Shared.Messages;

namespace Teatro.Shared.Bases.Etos;

public class PagedResultEto<T> : BrokerMessage, IPagedResultEto<T>
{
    public virtual int TotalCount { get; set; }
    
    private IReadOnlyList<T> _items;
    public IReadOnlyList<T> Items
    {
        get { return _items ??= new List<T>(); }
        set { _items = value; }
    }

    public PagedResultEto()
    {
    }

    public PagedResultEto(IReadOnlyList<T> items)
    {
        Items = items;
    }
}