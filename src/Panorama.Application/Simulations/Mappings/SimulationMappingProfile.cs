using System.Linq;
using AutoMapper;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations.Mappings;

public class SimulationMappingProfile : Profile
{
    public SimulationMappingProfile()
    {
        CreateMap<Simulation, ViewSimulationDto>()
            .ForMember(dest => dest.RunningCount, opt =>
            {
                opt.Condition(c => c.SimulationRuns is not null && c.SimulationRuns.Any());
                opt.MapFrom(src => src.SimulationRuns.Count);
            });
        
        CreateMap<CreateSimulationDto, Simulation>();
        
        CreateMap<UpdateSimulationDto, Simulation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}