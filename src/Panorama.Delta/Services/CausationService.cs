using Panorama.Delta.Shared.Requests;
using Panorama.Delta.Shared.Responses;
using Panorama.Delta.Shared.Services;

namespace Panorama.Causation.Services;

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