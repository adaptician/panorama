using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScene;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneErrored;
using Panorama.Scenes.Events.SceneReceived;

namespace Panorama.Backing.Bus.Scenes.SceneRequested;

public class SceneRequestedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneRequestedXto>
{
    public async Task Consume(ConsumeContext<SceneRequestedXto> context)
    {
        logger.Info($"Received scenes requested: {context.MessageId}");

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
            var carrier = sceneManager.CreateSceneReceivedCarrier();
            await carrier.Broadcast(new SceneReceivedEventData { 
                Data = mapper.Map<ViewSceneDto>(message.Data)
            }, userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(SceneRequestedXto)}";
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