using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.UpdateScene;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneErrored;
using Panorama.Scenes.Events.SceneUpdated;

namespace Panorama.Backing.Bus.Scenes.SceneUpdated;

public class SceneUpdatedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneUpdatedXto>
{
    public async Task Consume(ConsumeContext<SceneUpdatedXto> context)
    {
        logger.Info($"Received scene updated: {context.MessageId}");

        if (context.Message is null)
        {
            throw new Exception($"Unable to consume message - payload was invalid. " +
                                $"Message: {context.Message}");
        }
        
        var message = context.Message;

        using var scope = serviceProvider.CreateScope();
        var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();
        
        var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(message.UserCorrelationId);

        try
        {
            var carrier = sceneManager.CreateSceneUpdatedCarrier();
            await carrier.Broadcast(new SceneUpdatedEventData { 
                Data = mapper.Map<ViewSceneDto>(message.Data)
            }, userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(SceneUpdatedXto)}";
            logger.Error(errorMessage, e);
            
            var carrier = sceneManager.CreateSceneErroredCarrier();
            await carrier.Broadcast(new SceneErroredEventData
            {
                Error = new ErrorDto
                {
                    ErrorMessage = errorMessage
                }
            });
        }
        finally
        {
            await uow.CompleteAsync();
        }
    }
}