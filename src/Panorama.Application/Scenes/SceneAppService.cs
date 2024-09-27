using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using MediatR;
using Panorama.Authorization;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Scenes.Dto;
using Teatro.Shared.Scenes.Dtos;

namespace Panorama.Scenes;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SceneAppService(
    IScenographyProxy scenographyProxy, // TODO: remove
    IMediator mediatr
    ) : PanoramaAppServiceBase, ISceneAppService
{
    public async Task GetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken)
    {
        var mediateRequest = ObjectMapper.Map<ScenesRequested>(request);
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task GetById(long id, CancellationToken cancellationToken)
    {
        var mediateRequest = new SceneRequested { Id = id };
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task Create(CreateSceneDto input, CancellationToken cancellationToken)
    {
        var mediateRequest = ObjectMapper.Map<CreateSceneCommanded>(input);
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task Update(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        var mediateRequest = ObjectMapper.Map<UpdateSceneCommanded>(input);
        await mediatr.Send(mediateRequest, cancellationToken);
    }
    
    public async Task Delete(long id, CancellationToken cancellationToken)
    {
        var mediateRequest = new DeleteSceneCommanded { Id = id };
        await mediatr.Send(mediateRequest, cancellationToken);
    }
}