using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Panorama.Simulations.Dto;

public class ViewSimulationRunDto : EntityDto<long>
{
    public long SimulationId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public int ParticipantCount { get; set; }
    
    public virtual IEnumerable<ViewSimulationRunParticipantDto> Participants { get; set; }
}