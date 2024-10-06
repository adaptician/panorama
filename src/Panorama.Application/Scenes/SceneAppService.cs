using System;
using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MassTransit;
using Panorama.Authorization;
using Panorama.Backing.Bus.Shared.Scenes.Xto;
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
        
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:Scenes"));
        await endpoint.Send(new RequestScenesXto
        {
            MaxResultCount = request.MaxResultCount, 
            SkipCount = request.SkipCount,
            UserCorrelationId = user.CorrelationId
        }, cancellationToken);
    }

    // TODO: START HERE! Implement Get By Id
    // public async Task CommandGetById(long id, CancellationToken cancellationToken)
    // {
    //     var userId = AbpSession.GetUserId();
    //     var user = await UserManager.GetUserByIdAsync(userId);
    //     
    //     var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:Scenes"));
    //     await endpoint.Send(new RequestScenesXto
    //     {
    //         MaxResultCount = request.MaxResultCount, 
    //         SkipCount = request.SkipCount,
    //         UserCorrelationId = user.CorrelationId
    //     }, cancellationToken);
    // }
    //
    // public async Task<ViewSceneDto> Create(CreateSceneDto input, CancellationToken cancellationToken)
    // {
    //     return await scenographyProxy.CreateAsync(input, cancellationToken);
    // }
    //
    // public async Task Update(UpdateSceneDto input, CancellationToken cancellationToken)
    // {
    //     await scenographyProxy.UpdateAsync(input, cancellationToken);
    // }
    //
    // public async Task Delete(long id, CancellationToken cancellationToken)
    // {
    //     await scenographyProxy.DeleteAsync(id, cancellationToken);
    // }
}