using System;

namespace Panorama.Simulations.Dto;

public class ViewSimulationRunDto
{
    public long SimulationId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public int ParticipantCount { get; set; }
}