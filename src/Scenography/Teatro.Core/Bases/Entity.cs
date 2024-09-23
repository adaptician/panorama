using System.ComponentModel.DataAnnotations;

namespace Teatro.Core.Bases;

public abstract class Entity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}