using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

public interface ISimulationAppService : IApplicationService
{
    Task<PagedResultDto<GetSimulationDto>> GetAllSimulations(PagedSimulationResultRequestDto input);

    Task CreateSimulation(CreateSimulationDto input);
}