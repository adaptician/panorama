using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;
using Panorama.Events.Errors;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneCreated;

namespace Panorama.Backing.Bus.Scenes.SceneCreated;

public class SceneCreatedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneCreatedXto>
{
    public async Task Consume(ConsumeContext<SceneCreatedXto> context)
    {
        logger.Info($"Received scene created: {context.MessageId}");

        if (context.Message is null)
        {
            throw new Exception($"Unable to consume message - payload was invalid. " +
                                $"Message: {context.Message}");
        }
        
        var message = context.Message;

        using var scope = serviceProvider.CreateScope();
        var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();
        
        try
        {
            var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(message.UserCorrelationId);
            
            var carrier = sceneManager.CreateSceneCreatedCarrier();
            await carrier.Broadcast(new SceneCreatedEventData
            {
                Data = mapper.Map<ViewSceneDto>(message.Data)
            }, userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(SceneCreatedXto)}";
            logger.Error(errorMessage, e);
            
            var carrier = sceneManager.CreateErroredCarrier();
            await carrier.Broadcast(new ErroredEventData
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