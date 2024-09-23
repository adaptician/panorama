namespace Teatro.Core.Bases;

public abstract class AuditDeletionEntity<TKey> : AuditModificationEntity<TKey>
{
    public bool IsDeleted { get; set; }
}