using Abp.Application.Services.Dto;
using Panorama.Simulations.Dto.Base;

namespace Panorama.Simulations.Dto;

public class UpdateSimulationDto : MutateSimulationDto, IEntityDto<long>
{
    public long Id { get; set; }
}