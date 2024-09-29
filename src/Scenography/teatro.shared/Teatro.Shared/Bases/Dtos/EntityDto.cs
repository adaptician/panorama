namespace Teatro.Shared.Bases.Dtos;

public abstract class EntityDto<TKey>
{
    public TKey Id { get; set; }
}