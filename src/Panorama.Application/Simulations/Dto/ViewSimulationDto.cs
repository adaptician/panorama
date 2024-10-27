using Abp.Domain.Entities;
using Panorama.Backing.Bus.Shared.Common.Dto;

namespace Panorama.Simulations.Dto;

public class ViewSimulationDto : EntityDto<long>, IMustHaveTenant
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string SceneCorrelationId { get; set; }
    
    public int RunningCount { get; set; }

    public int TenantId { get; set; }
}