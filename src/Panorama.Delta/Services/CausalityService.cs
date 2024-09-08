using Panorama.Delta.Shared.Causality;
using Panorama.Delta.Shared.Causality.Requests;
using Panorama.Delta.Shared.Causality.Responses;

namespace Panorama.Causation.Services;

/// <summary>
/// TODO: Causality service (12)
/// </summary>
public class CausalityService : ICausalityService
{
    public Task<IPaginatedCausalityResponse> Get(IFilterCausalityRequest request)
    {
        throw new NotImplementedException("TODO: implement GET paginated and filtered causality (3)");
        // MongoDb resource + entity + repository
        // Expose on controller
        // Permission? via ABP - now or later? A true test would be with the relay
    }

    public Task<ICausality> Get(long id)
    {
        throw new NotImplementedException("TODO: implement GET one causality. (1)");
        // API test only
    }

    public Task<ICausalityValidationResponse> Validate(IValidateCausalityRequest request)
    {
        throw new NotImplementedException("TODO: implement VALIDATE causality. (3)");
        // BASIC FIRST!!!
        // API test only
        // Think about how it will be triggered, and how to scoop extended logic.
    }

    public Task<long> Create(ICreateCausalityRequest request)
    {
        throw new NotImplementedException("TODO: implement CREATE Causality. (2)");
        // into MongoDb
        // into cache
        // Think about the payload - what needs to be persisted and in what format.
    }

    public Task<long> Update(IUpdateCausalityRequest request)
    {
        throw new NotImplementedException("TODO: implement UPDATE Causality. (2)");
        // from and into MongoDb
        // into cache
        // Nothing fancy here - MVP.
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException("TODO: implement DELETE Causality. (1)");
        // Are there/can there be soft deletes in MongoDb? Not important, just curious.
        // delete from MongoDb
        // Clear from cache
    }
}