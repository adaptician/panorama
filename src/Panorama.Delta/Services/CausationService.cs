using Panorama.Delta.Shared.Causation;
using Panorama.Delta.Shared.Causation.Requests;
using Panorama.Delta.Shared.Causation.Responses;

namespace Panorama.Causation.Services;

/// <summary>
/// TODO: Causation service (5)
/// </summary>
public class CausationService : ICausationService
{
    public Task<ICausedResponse> Submit(ICausedRequest request)
    {
        throw new NotImplementedException("TODO: implement Causation Submit (5)");
        // check cache?
        // recognised?
        // validate
        // send to bus producer
    }
}