using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

public interface ISimulationAppService : IApplicationService
{
    Task<PagedResultDto<ViewSimulationDto>> GetAllSimulations(PagedSimulationResultRequestDto input);

    Task<ViewSimulationDto> GetSimulationById(long simulationId);
    
    Task CreateSimulation(CreateSimulationDto input);

    Task UpdateSimulation(UpdateSimulationDto input);

    Task DeleteSimulation(long simulationId);
}