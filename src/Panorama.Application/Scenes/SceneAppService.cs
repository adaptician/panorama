using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using MediatR;
using Newtonsoft.Json;
using Panorama.Authorization;
using Panorama.Backing.Shared.Scenes.Requests.Mediations;
using Panorama.Common.Constants;
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
        var data = ObjectMapper.Map<ScenesRequested>(request);
        var mediateRequest = new ScenesOperation
        {
            Operation = BrokerMessageOperations.GetAll,
            Data = JsonConvert.SerializeObject(data)
        };
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task GetById(long id, CancellationToken cancellationToken)
    {
        var data = new SceneRequested { Id = id };
        var mediateRequest = new ScenesOperation
        {
            Operation = BrokerMessageOperations.Get,
            Data = JsonConvert.SerializeObject(data)
        };
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task Create(CreateSceneDto input, CancellationToken cancellationToken)
    {
        var data = ObjectMapper.Map<CreateSceneCommanded>(input);
        var mediateRequest = new ScenesOperation
        {
            Operation = BrokerMessageOperations.Create,
            Data = JsonConvert.SerializeObject(data)
        };
        await mediatr.Send(mediateRequest, cancellationToken);
    }

    public async Task Update(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        var data = ObjectMapper.Map<UpdateSceneCommanded>(input);
        var mediateRequest = new ScenesOperation
        {
            Operation = BrokerMessageOperations.Update,
            Data = JsonConvert.SerializeObject(data)
        };
        await mediatr.Send(mediateRequest, cancellationToken);
    }
    
    public async Task Delete(long id, CancellationToken cancellationToken)
    {
        var data = new DeleteSceneCommanded { Id = id };
        var mediateRequest = new ScenesOperation
        {
            Operation = BrokerMessageOperations.Delete,
            Data = JsonConvert.SerializeObject(data)
        };
        await mediatr.Send(mediateRequest, cancellationToken);
    }
}