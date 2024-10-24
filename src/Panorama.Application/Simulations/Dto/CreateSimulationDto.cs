using System.ComponentModel.DataAnnotations;
using Panorama.Core.Shared.Simulations;

namespace Panorama.Simulations.Dto;

public class CreateSimulationDto
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