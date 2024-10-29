using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Timing.Timezone;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Panorama.Authorization;
using Panorama.Core.Shared.Simulations;
using Panorama.Simulations.Dto;
using Panorama.Simulations.Interfaces;

namespace Panorama.Simulations;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SimulationRunAppService : PanoramaAppServiceBase, ISimulationRunAppService
{
    private readonly IRepository<Simulation, long> _simulationRepository;
    private readonly IRepository<SimulationRun, long> _simulationRunRepository;
    private readonly ITimeZoneConverter _timeZoneConverter;

    public SimulationRunAppService(
        IRepository<Simulation, long> simulationRepository, 
        IRepository<SimulationRun, long> simulationRunRepository, 
        ITimeZoneConverter timeZoneConverter)
    {
        _simulationRepository = simulationRepository;
        _simulationRunRepository = simulationRunRepository;
        _timeZoneConverter = timeZoneConverter;
    }

    public async Task<IEnumerable<ViewSimulationRunDto>> GetAllSimulationRuns(long simulationId)
    {
        var tenantId = AbpSession.TenantId;
        var userId = AbpSession.UserId.HasValue
            ? AbpSession.UserId.Value
            : throw new Exception("User is required for timezone conversion.");
        
        var runs = await _simulationRunRepository
            .GetAll()
            .Include(i => i.SimulationRunParticipants)
            .Where(x => x.SimulationId == simulationId)
            .ToListAsync();

        var results = new List<ViewSimulationRunDto>();
        
        foreach (var run in runs)
        {
            var mapped = ObjectMapper.Map<ViewSimulationRunDto>(run);
            var startTime = _timeZoneConverter.Convert(run.StartTime, tenantId, userId);
            mapped.StartTime = startTime ?? throw new Exception("Timezone conversion failed.");
            
            var endTime = _timeZoneConverter.Convert(run.EndTime, tenantId, userId);
            mapped.EndTime = endTime;
            
            results.Add(mapped);
        }
        
        return results;
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Running_Start)]
    public async Task StartRun(long simulationId)
    {
        var existing = await _simulationRepository
            .GetAll()
            .Include(i => i.SimulationRuns)
            .SingleOrDefaultAsync(x => x.Id == simulationId);

        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to run ${nameof(Simulation)} with Id ${simulationId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }
        
        var hasRunning = existing.SimulationRuns is not null && existing.SimulationRuns.Any();

        if (hasRunning && existing.SimulationRuns.Count >= SimulationConstants.MaxConcurrentRunningSimulations)
        {
            throw new UserFriendlyException(L("MaximumConcurrentSimulationsRunningReached"));
        }

        var run = new SimulationRun(simulationId);
        
        existing.SimulationRuns ??= new List<SimulationRun>();
        existing.SimulationRuns.Add(run);

        await CurrentUnitOfWork.SaveChangesAsync();
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Running_Participate)]
    public async Task JoinSimulation(long simulationRunId)
    {
        var existing = await _simulationRunRepository
            .GetAll()
            .Include(i => i.SimulationRunParticipants)
            .SingleOrDefaultAsync(x => x.Id == simulationRunId);

        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to stop ${nameof(SimulationRun)} with Id ${simulationRunId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        var userId = AbpSession.UserId;
        if (userId is null)
        {
            throw new UserFriendlyException(L("UnableToDetermineUserFromSession"));
        }
        
        var participant = new SimulationRunParticipant(simulationRunId, userId.Value);

        existing.SimulationRunParticipants ??= new List<SimulationRunParticipant>();
        existing.SimulationRunParticipants.Add(participant);
        
        await CurrentUnitOfWork.SaveChangesAsync();
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Running_Participate)]
    public async Task LeaveSimulation(long simulationRunId)
    {
        var existing = await _simulationRunRepository
            .GetAll()
            .Include(i => i.SimulationRunParticipants)
            .SingleOrDefaultAsync(x => x.Id == simulationRunId);

        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to stop ${nameof(SimulationRun)} with Id ${simulationRunId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        if (existing.SimulationRunParticipants is null || !existing.SimulationRunParticipants.Any())
        {
            throw new UserFriendlyException(L("UserParticipantNotFound"));
        }

        var userId = AbpSession.UserId;
        var participant = existing.SimulationRunParticipants.FirstOrDefault(x => x.UserId == userId);

        if (participant is null)
        {
            throw new UserFriendlyException(L("UserParticipantNotFound"));
        }

        participant.ExitTime = DateTime.UtcNow;
        participant.IsDeleted = true;
        
        await CurrentUnitOfWork.SaveChangesAsync();
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Simulations_Running_Stop)]
    public async Task StopRun(long simulationRunId)
    {
        var existing = await _simulationRunRepository
            .GetAll()
            .Include(i => i.SimulationRunParticipants)
            .SingleOrDefaultAsync(x => x.Id == simulationRunId);

        if (existing is null)
        {
            Logger.Error($"An error occurred while trying to stop ${nameof(SimulationRun)} with Id ${simulationRunId} - " +
                         $"simulation was not found.");
            throw new UserFriendlyException(L("SimulationNotFound"));
        }

        existing.EndTime = DateTime.UtcNow;
        existing.IsDeleted = true;
        
        foreach (var participant in existing.SimulationRunParticipants)
        {
            participant.IsDeleted = true;
        }
        
        await CurrentUnitOfWork.SaveChangesAsync();
    }
}