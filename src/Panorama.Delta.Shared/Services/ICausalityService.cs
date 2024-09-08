using Panorama.Delta.Shared.Requests;

namespace Panorama.Delta.Shared.Services;

public interface ICausalityService
{
    Task Get(IFilterCausalityRequest request);

    Task Get(long id);

    Task Validate(IValidateCausalityRequest request);
    
    Task Create(ICreateCausalityRequest request);

    Task Update(IUpdateCausalityRequest request);

    Task Delete(long id);
}