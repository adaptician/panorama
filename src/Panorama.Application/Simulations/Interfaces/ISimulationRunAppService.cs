using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations.Interfaces;

public interface ISimulationRunAppService : IApplicationService
{
    Task<IEnumerable<ViewSimulationRunDto>> GetAllSimulationRuns(long simulationId);
    
    Task StartRun(long simulationId);

    Task JoinSimulation(long simulationRunId);

    Task LeaveSimulation(long simulationRunId);

    Task StopRun(long simulationRunId);
}