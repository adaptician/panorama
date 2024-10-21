using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Panorama.Core.Shared.Simulations;

namespace Panorama.Simulations;

[Table(nameof(Simulation), Schema = SchemaNames.Panorama)]
public class Simulation : FullAuditedEntity<long>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SimulationConstants.MaxNameLength)]
    public string Name { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SimulationConstants.MaxDescriptionLength)]
    public string Description { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [MaxLength(SimulationConstants.MaxCorrelationIdLength)]
    public string SceneCorrelationId { get; set; }
}