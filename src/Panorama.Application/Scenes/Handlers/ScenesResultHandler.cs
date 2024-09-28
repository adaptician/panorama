using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Panorama.Authorization.Users;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Dto;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Scenes.Handlers;

public class ScenesResultHandler(
    ILogger<ScenesResultHandler> logger,
    IServiceProvider serviceProvider
    ) : IProcessMessageHandler<ScenesResultEto>
{
    public async Task ProcessMessageAsync(ScenesResultEto request)
    {
        logger.LogTrace($"A request to process {nameof(ScenesResultEto)} was received. " +
                        $"Request {MediationActionEnum.Received.GetCode()}");
        
        if (request == null || string.IsNullOrEmpty(request.Data))
        {
            logger.LogWarning($"A request to process {nameof(ScenesResultEto)} was received as invalid." +
                              $"Request {MediationActionEnum.Ignored.GetCode()}");

            return; // Go no further.
        }
        
        using var scope = serviceProvider.CreateScope();

        var data = new ScenesReceivedEventData
        {
            Data = JsonConvert.DeserializeObject<PagedResultDto<ViewSceneDto>>(request.Data)
        };
        
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager>();
        var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(request.UserId);
        
        var sceneManager = scope.ServiceProvider.GetRequiredService<SceneManager>();
        var carrier = sceneManager.CreateScenesReceivedCarrier();
        await carrier.Broadcast(data, userIdentifier);
    }
}