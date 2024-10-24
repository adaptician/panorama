using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

public interface ISimulationAppService : IApplicationService
{
    Task<PagedResultDto<GetSimulationDto>> GetAll(PagedSimulationResultRequestDto input);

    Task Create(CreateSimulationDto input);
}