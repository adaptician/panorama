using AutoMapper;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations;

public class SimulationMappingProfile : Profile
{
    public SimulationMappingProfile()
    {
        CreateMap<Simulation, ViewSimulationDto>();
        
        CreateMap<CreateSimulationDto, Simulation>();
        
        CreateMap<UpdateSimulationDto, Simulation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}