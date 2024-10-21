using System;
using System.Threading;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using MassTransit;
using Panorama.Authorization;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;
using Panorama.Backing.Bus.Shared.Scenes.Xto.DeleteScene;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScene;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScenes;
using Panorama.Backing.Bus.Shared.Scenes.Xto.UpdateScene;
using Panorama.Scenes.Dto;
using Teatro.Contracts;

namespace Panorama.Scenes;

[AbpAuthorize(PermissionNames.Pages_Tenant_Scenes)]
public class SceneAppService(
    ISendEndpointProvider sendEndpointProvider
) : PanoramaAppServiceBase, ISceneAppService
{
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Scenes_View)]
    public async Task CommandGetAll(PagedSceneResultRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = AbpSession.GetUserId();
            var user = await UserManager.GetUserByIdAsync(userId);
        
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(SceneMessageUrn.Queue_RetrieveScenes));
            await endpoint.Send(new RetrieveScenesXto
            {
                MaxResultCount = request.MaxResultCount, 
                SkipCount = request.SkipCount,
                UserCorrelationId = user.CorrelationId
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error(L("MessagePublishingFailed"), e);
            throw new UserFriendlyException(L("MessagePublishingFailed"));
        }
    }

    [AbpAuthorize(PermissionNames.Pages_Tenant_Scenes_View)]
    public async Task CommandGetById(string correlationId, CancellationToken cancellationToken)
    {
        try
        {
            var userId = AbpSession.GetUserId();
            var user = await UserManager.GetUserByIdAsync(userId);
        
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(SceneMessageUrn.Queue_RetrieveScene));
            await endpoint.Send(new RetrieveSceneXto
            {
                SceneCorrelationId = correlationId,
                UserCorrelationId = user.CorrelationId
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error(L("MessagePublishingFailed"), e);
            throw new UserFriendlyException(L("MessagePublishingFailed"));
        }
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Scenes_Create)]
    public async Task CommandCreate(CreateSceneDto input, CancellationToken cancellationToken)
    {
        try
        {
            var userId = AbpSession.GetUserId();
            var user = await UserManager.GetUserByIdAsync(userId);
        
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(SceneMessageUrn.Queue_CreateScene));
            await endpoint.Send(new CreateSceneXto()
            {
                Name = input.Name,
                Description = input.Description,
                SceneData = input.SceneData,
                UserCorrelationId = user.CorrelationId
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error(L("MessagePublishingFailed"), e);
            throw new UserFriendlyException(L("MessagePublishingFailed"));
        }
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Scenes_Update)]
    public async Task CommandUpdate(UpdateSceneDto input, CancellationToken cancellationToken)
    {
        try
        {
            var userId = AbpSession.GetUserId();
            var user = await UserManager.GetUserByIdAsync(userId);
        
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(SceneMessageUrn.Queue_UpdateScene));
            await endpoint.Send(new UpdateSceneXto()
            {
                SceneCorrelationId = input.CorrelationId,
                Name = input.Name,
                Description = input.Description,
                UserCorrelationId = user.CorrelationId
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error(L("MessagePublishingFailed"), e);
            throw new UserFriendlyException(L("MessagePublishingFailed"));
        }
    }
    
    [AbpAuthorize(PermissionNames.Pages_Tenant_Scenes_Delete)]
    public async Task CommandDelete(string correlationId, CancellationToken cancellationToken)
    {
        try
        {
            var userId = AbpSession.GetUserId();
            var user = await UserManager.GetUserByIdAsync(userId);
        
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(SceneMessageUrn.Queue_DeleteScene));
            await endpoint.Send(new DeleteSceneXto()
            {
                SceneCorrelationId = correlationId,
                UserCorrelationId = user.CorrelationId
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.Error(L("MessagePublishingFailed"), e);
            throw new UserFriendlyException(L("MessagePublishingFailed"));
        }
    }
}