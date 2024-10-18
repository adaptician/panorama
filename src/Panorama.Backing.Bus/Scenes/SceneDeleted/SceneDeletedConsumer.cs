using Abp.Domain.Uow;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Castle.Core.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.DeleteScene;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneDeleted;
using Panorama.Scenes.Events.SceneErrored;

namespace Panorama.Backing.Bus.Scenes.SceneDeleted;

public class SceneDeletedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneDeletedXto>
{
    public async Task Consume(ConsumeContext<SceneDeletedXto> context)
    {
        logger.Info($"Received scene deleted: {context.MessageId}");

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
            var carrier = sceneManager.CreateSceneDeletedCarrier();
            await carrier.Broadcast(new SceneDeletedEventData(), userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(SceneDeletedXto)}";
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