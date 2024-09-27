using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using MediatR;
using Panorama.Authorization;
using Panorama.Backing.Shared.Scenes.Requests;
using Panorama.Scenes.Dto;
using Teatro.Shared.Bases.Dtos;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SceneAppService(
    IScenographyProxy scenographyProxy, // TODO: remove
    IMediator mediatr
    ) : PanoramaAppServiceBase, ISceneAppService
{
    public async Task RequestGetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken)
    {
        var mediateRequest = ObjectMapper.Map<ScenesRequested>(request);
        await mediatr.Send(mediateRequest, cancellationToken);
    }
    
    // TODO: remove
    public async Task<PagedResultDto<ViewSceneDto>> GetAll(PagedSceneResultRequestDto request,
        CancellationToken cancellationToken)
    {
        // TODO: just testing, clean up.
        var mediateRequest = ObjectMapper.Map<ScenesRequested>(request);
        await mediatr.Send(mediateRequest, cancellationToken);
        
        return await scenographyProxy.GetAllAsync(request, cancellationToken);
    }

    public async Task<ViewSceneDto> GetById(long id, CancellationToken cancellationToken)
    {
        return await scenographyProxy.GetByIdAsync(id, cancellationToken);
    }

    public async Task<ViewSceneDto> Create(CreateSceneDto input, CancellationToken cancellationToken)
    {
        return await scenographyProxy.CreateAsync(input, cancellationToken);
    }

    public async Task Update(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        await scenographyProxy.UpdateAsync(input, cancellationToken);
    }
    
    public async Task Delete(long id, CancellationToken cancellationToken)
    {
        await scenographyProxy.DeleteAsync(id, cancellationToken);
    }
}