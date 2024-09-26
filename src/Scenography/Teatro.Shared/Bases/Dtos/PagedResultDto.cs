using System.ComponentModel.DataAnnotations;

namespace Teatro.Shared.Bases.Dtos;

[Serializable]
public class PagedResultDto<T>
{
    public virtual int TotalCount { get; set; }
    
    private IReadOnlyList<T> _items;
    public IReadOnlyList<T> Items
    {
        get { return _items ??= new List<T>(); }
        set { _items = value; }
    }

    public PagedResultDto()
    {
    }

    public PagedResultDto(IReadOnlyList<T> items)
    {
        Items = items;
    }
}