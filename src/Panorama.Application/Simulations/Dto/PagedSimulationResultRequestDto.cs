using Abp.Application.Services.Dto;

namespace Panorama.Simulations.Dto;

public class PagedSimulationResultRequestDto : PagedResultRequestDto
{
    public string Keyword { get; set; }

    public bool HasRunning { get; set; } = false;
}