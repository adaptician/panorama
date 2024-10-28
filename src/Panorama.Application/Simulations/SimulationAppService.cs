using System;
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
using Panorama.Core.Shared.Simulations;
using Panorama.Simulations.Dto;
using Panorama.Simulations.Interfaces;

namespace Panorama.Simulations;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SimulationAppService : PanoramaAppServiceBase, ISimulationAppService
{
    private readonly IRepository<Simulation, long> _simulationRepository;

    public SimulationAppService(IRepository<Simulation, long> simulationRepository
        )
    {
        _simulationRepository = simulationRepository;
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_View)]
    public async Task<PagedResultDto<ViewSimulationDto>> GetAllSimulations(PagedSimulationResultRequestDto input)
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
        
        var totalCount = await query.CountAsync();
        var result = await pagedQuery.ToListAsync();

        return new PagedResultDto<ViewSimulationDto>(
            totalCount,
            ObjectMapper.Map<List<ViewSimulationDto>>(result)
        );
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_View)]
    public async Task<ViewSimulationDto> GetSimulationById(long simulationId)
    {
        var existing = await _simulationRepository
            .GetAll()
            .Include(i => i.SimulationRuns)
            .SingleOrDefaultAsync(x => x.Id == simulationId);
        
        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to retrieve ${nameof(Simulation)} with Id ${simulationId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        return ObjectMapper.Map<ViewSimulationDto>(existing);
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Create)]
    public async Task CreateSimulation(CreateSimulationDto input)
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

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Update)]
    public async Task UpdateSimulation(UpdateSimulationDto input)
    {
        if (input is null)
        {
            throw new UserFriendlyException(L("BadRequest"));
        }

        var existing = await _simulationRepository
            .GetAll()
            .Include(i => i.SimulationRuns)
            .SingleOrDefaultAsync(x => x.Id == input.Id);

        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to update ${nameof(Simulation)} with Id ${input.Id} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        var hasRunning = existing.SimulationRuns is not null && existing.SimulationRuns.Count > 0;

        if (hasRunning && !input.SceneCorrelationId.Equals(existing.SceneCorrelationId))
        {
            throw new UserFriendlyException(L("SceneCannotBeUpdatedForSimulationWithRunning"));
        }
        
        ObjectMapper.Map(input, existing);

        await CurrentUnitOfWork.SaveChangesAsync();
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Delete)]
    public async Task DeleteSimulation(long simulationId)
    {
        var existing = await _simulationRepository
            .GetAll()
            .SingleOrDefaultAsync(x => x.Id == simulationId);
        
        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to delete ${nameof(Simulation)} with Id ${simulationId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        existing.IsDeleted = true;
        
        await CurrentUnitOfWork.SaveChangesAsync();
    }
}