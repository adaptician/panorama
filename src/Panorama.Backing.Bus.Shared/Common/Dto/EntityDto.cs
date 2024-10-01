namespace Panorama.Backing.Bus.Shared.Common.Dto;

public abstract class EntityDto<TKey>
{
    public TKey Id { get; set; }
}