namespace Teatro.Core.Bases;

public abstract class AuditCreationEntity<TKey> : Entity<TKey>
{
    public DateTime CreationTime { get; set; }
}