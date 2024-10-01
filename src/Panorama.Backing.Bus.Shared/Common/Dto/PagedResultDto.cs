using Newtonsoft.Json;

namespace Panorama.Backing.Bus.Shared.Common.Dto;

[Serializable]
public class PagedResultDto<T>
{
    [JsonProperty("totalCount")]
    public virtual int TotalCount { get; set; }
    
    private IReadOnlyList<T> _items;
    [JsonProperty("items")]
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