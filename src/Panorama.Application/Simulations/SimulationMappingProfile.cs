using AutoMapper;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

public class SimulationMappingProfile : Profile
{
    public SimulationMappingProfile()
    {
        CreateMap<Simulation, GetSimulationDto>();
        
        CreateMap<CreateSimulationDto, Simulation>();
    }
}