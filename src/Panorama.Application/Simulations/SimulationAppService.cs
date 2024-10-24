using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Panorama.Authorization;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SimulationAppService : PanoramaAppServiceBase, ISimulationAppService
{
    private readonly IRepository<Simulation, long> _simulationRepository;

    public SimulationAppService(IRepository<Simulation, long> simulationRepository)
    {
        _simulationRepository = simulationRepository;
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_View)]
    public async Task<PagedResultDto<GetSimulationDto>> GetAll(PagedSimulationResultRequestDto input)
    {
        if (input is null)
        {
            throw new UserFriendlyException(L("BadRequest"));
        }
        
        var query = _simulationRepository.GetAll()
            .Include(i => i.SimulationRuns)
            .WhereIf(input.HasRunning, x => x.SimulationRuns.Count > 0)
            .WhereIf(!string.IsNullOrEmpty(input.Keyword), x =>
                x.Name.ToLower().Contains(input.Keyword.ToLower())
                || x.Description.ToLower().Contains(input.Keyword.ToLower()));

        var pagedQuery = query
            .PageBy(input);
        
        var totalCount = await pagedQuery.CountAsync();
        var result = await pagedQuery.ToListAsync();

        return new PagedResultDto<GetSimulationDto>(
            totalCount,
            ObjectMapper.Map<List<GetSimulationDto>>(result)
        );
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Create)]
    public async Task Create(CreateSimulationDto input)
    {
        if (input is null)
        {
            throw new UserFriendlyException(L("BadRequest"));
        }

        var entity = ObjectMapper.Map<Simulation>(input);

        var exists = await _simulationRepository.GetAll().AnyAsync(x => x.Name.Equals(entity.Name));

        if (exists)
        {
            throw new UserFriendlyException(L("SimulationWithNameExists"));
        }

        await _simulationRepository.InsertAsync(entity);
    }
}