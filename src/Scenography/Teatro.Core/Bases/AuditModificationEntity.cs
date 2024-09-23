namespace Teatro.Core.Bases;

public abstract class AuditModificationEntity<TKey> : AuditCreationEntity<TKey>
{
    public DateTime? LastModificationTime { get; set; }
}