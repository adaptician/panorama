using System.ComponentModel.DataAnnotations.Schema;
using Teatro.Core.Bases;

namespace Teatro.Core.Scenography;

[Table(nameof(Scenography), Schema = SchemaNames.Document)]
public class Scenography : FullyAuditedEntity<long>
{
    public Guid DocumentId { get; set; } = Guid.NewGuid();
}