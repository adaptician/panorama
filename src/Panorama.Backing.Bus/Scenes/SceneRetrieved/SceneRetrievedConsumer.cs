using Abp.Domain.Uow;
using AutoMapper;
using Castle.Core.Logging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RetrieveScene;
using Panorama.Events.Errors;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneRetrieved;

namespace Panorama.Backing.Bus.Scenes.SceneRetrieved;

public class SceneRetrievedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneRetrievedXto>
{
    public async Task Consume(ConsumeContext<SceneRetrievedXto> context)
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
        
        try
        {
            var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(message.UserCorrelationId);
            
            var carrier = sceneManager.CreateSceneReceivedCarrier();
            await carrier.Broadcast(new SceneRetrievedEventData { 
                Data = mapper.Map<ViewSceneDto>(message.Data)
            }, userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(SceneRetrievedXto)}";
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