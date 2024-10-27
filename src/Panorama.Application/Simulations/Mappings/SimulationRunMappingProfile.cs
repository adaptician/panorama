using System.Linq;
using AutoMapper;
using Panorama.Simulations.Dto;

namespace Panorama.Simulations.Mappings;

public class SimulationRunMappingProfile : Profile
{
    public SimulationRunMappingProfile()
    {
        CreateMap<SimulationRun, ViewSimulationRunDto>()
            .ForMember(dest => dest.ParticipantCount, opt =>
            {
                opt.Condition(c => c.SimulationRunParticipants is not null && c.SimulationRunParticipants.Any());
                    opt.MapFrom(src => src.SimulationRunParticipants.Count);
            });
    }
}