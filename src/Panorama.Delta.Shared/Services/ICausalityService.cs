using Panorama.Delta.Shared.Requests;

namespace Panorama.Delta.Shared.Services;

/// <summary>
/// A Causality service is a CRUD service.
/// Effect payloads are constructed through the service, attached to catalyst criteria.
/// Valid Causes are persisted into a store, and synced to a caching service.
/// </summary>
public interface ICausalityService
{
    Task Get(IFilterCausalityRequest request);

    Task Get(long id);

    Task Validate(IValidateCausalityRequest request);
    
    Task Create(ICreateCausalityRequest request);

    Task Update(IUpdateCausalityRequest request);

    Task Delete(long id);
}