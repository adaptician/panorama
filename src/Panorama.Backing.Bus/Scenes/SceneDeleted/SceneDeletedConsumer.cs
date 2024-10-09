using Abp.Domain.Uow;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Scenes.Xto.DeleteScene;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneDeleted;

namespace Panorama.Backing.Bus.Scenes.SceneDeleted;

public class SceneDeletedConsumer(ILogger<SceneDeletedXto> logger,
    IServiceProvider serviceProvider,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneDeletedXto>
{
    public async Task Consume(ConsumeContext<SceneDeletedXto> context)
    {
        logger.LogInformation("Received scene deleted: {messageId}",
            context.MessageId
        );

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

        var carrier = sceneManager.CreateSceneDeletedCarrier();
        await carrier.Broadcast(new SceneDeletedEventData(), userIdentifier);
        
        await uow.CompleteAsync();
    }
}