using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.CreateScene;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneCreated;

namespace Panorama.Backing.Bus.Scenes.SceneCreated;

public class SceneCreatedConsumer(ILogger<SceneCreatedXto> logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneCreatedXto>
{
    public async Task Consume(ConsumeContext<SceneCreatedXto> context)
    {
        logger.LogInformation("Received scene created: {messageId}",
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

        var carrier = sceneManager.CreateSceneCreatedCarrier();
        await carrier.Broadcast(new SceneCreatedEventData { 
            Data = mapper.Map<ViewSceneDto>(message.Data)
        }, userIdentifier);
        
        await uow.CompleteAsync();
    }
}