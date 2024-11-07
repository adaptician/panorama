using Panorama.Core.Shared.Common;

namespace Panorama.Core.Shared.Simulations;

public static class SimulationConstants
{
    public const int MaxNameLength = BaseConstants.MaxNameLength;
    public const int MaxDescriptionLength = BaseConstants.MaxDescriptionLength;
    public const int MaxCorrelationIdLength = BaseConstants.MaxCorrelationIdLength;
    
    public const int MaxConcurrentRunningSimulations = 3;
}