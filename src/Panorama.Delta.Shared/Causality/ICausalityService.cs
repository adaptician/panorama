using Panorama.Delta.Shared.Causality.Requests;
using Panorama.Delta.Shared.Causality.Responses;

namespace Panorama.Delta.Shared.Causality;

/// <summary>
/// A Causality service is a CRUD service.
/// Effect payloads are constructed through the service, attached to catalyst criteria.
/// Valid Causes are persisted into a store, and synced to a caching service.
/// </summary>
public interface ICausalityService
{
    Task<IPaginatedCausalityResponse> Get(IFilterCausalityRequest request);

    Task<ICausality> Get(long id);

    Task<ICausalityValidationResponse> Validate(IValidateCausalityRequest request);
    
    Task<long> Create(ICreateCausalityRequest request);

    Task<long> Update(IUpdateCausalityRequest request);

    Task Delete(long id);
}