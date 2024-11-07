using System;
using Abp.Application.Services.Dto;

namespace Panorama.Simulations.Dto;

public class ViewSimulationRunParticipantDto : EntityDto<long>
{
    public long SimulationRunId { get; set; }
    
    public long UserId { get; set; }
    
    public DateTime EntryTime { get; set; }
    
    public DateTime? ExitTime { get; set; }
}