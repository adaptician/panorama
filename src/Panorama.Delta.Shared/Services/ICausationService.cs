using Panorama.Delta.Shared.Requests;
using Panorama.Delta.Shared.Responses;

namespace Panorama.Delta.Shared.Services;

/// <summary>
/// A Causation Service is a receiving service.
/// It is responsible for receiving Cause events, recognising them,
/// and then handing them off to be persisted and propagated. 
/// </summary>
public interface ICausationService
{
    Task<ICausedResponse> Submit(ICausedRequest request);
}