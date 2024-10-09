using System;
using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MassTransit;
using Panorama.Authorization;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScene;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScenes;
using Panorama.Backing.Bus.Shared.Scenes.Xto.UpdateScene;
using Panorama.Scenes.Dto;

namespace Panorama.Scenes;

[AbpAuthorize(PermissionNames.Pages_Tenant_Simulations)]
public class SceneAppService(
    ISendEndpointProvider sendEndpointProvider
) : PanoramaAppServiceBase, ISceneAppService
{
    
    public async Task CommandGetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken)
    {
        var userId = AbpSession.GetUserId();
        var user = await UserManager.GetUserByIdAsync(userId);
        
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:RequestScenes"));
        await endpoint.Send(new RequestScenesXto
        {
            MaxResultCount = request.MaxResultCount, 
            SkipCount = request.SkipCount,
            UserCorrelationId = user.CorrelationId
        }, cancellationToken);
    }

    public async Task CommandGetById(string correlationId, CancellationToken cancellationToken)
    {
        var userId = AbpSession.GetUserId();
        var user = await UserManager.GetUserByIdAsync(userId);
        
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:RequestScene"));
        await endpoint.Send(new RequestSceneXto
        {
            SceneCorrelationId = correlationId,
            UserCorrelationId = user.CorrelationId
        }, cancellationToken);
    }
    
    public async Task CommandCreate(CreateSceneDto input, CancellationToken cancellationToken)
    {
        var userId = AbpSession.GetUserId();
        var user = await UserManager.GetUserByIdAsync(userId);
        
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:CreateScene"));
        await endpoint.Send(new CreateSceneXto()
        {
            Name = input.Name,
            Description = input.Description,
            SceneData = input.SceneData,
            UserCorrelationId = user.CorrelationId
        }, cancellationToken);
    }
    
    public async Task CommandUpdate(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        var userId = AbpSession.GetUserId();
        var user = await UserManager.GetUserByIdAsync(userId);
        
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:UpdateScene"));
        await endpoint.Send(new UpdateSceneXto()
        {
            SceneCorrelationId = input.CorrelationId,
            Name = input.Name,
            Description = input.Description,
            UserCorrelationId = user.CorrelationId
        }, cancellationToken);
    }
    //
    // public async Task Delete(long id, CancellationToken cancellationToken)
    // {
    //     await scenographyProxy.DeleteAsync(id, cancellationToken);
    // }
}