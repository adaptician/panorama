using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto;
using Panorama.Scenes;
using Panorama.Scenes.Events.SceneReceived;

namespace Panorama.Backing.Bus.Scenes.SceneRequested;

public class SceneRequestedConsumer(ILogger<SceneRequestedXto> logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
) 
    : IConsumer<SceneRequestedXto>
{
    public async Task Consume(ConsumeContext<SceneRequestedXto> context)
    {
        logger.LogInformation("Received scenes requested: {messageId}",
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

        var carrier = sceneManager.CreateSceneReceivedCarrier();
        await carrier.Broadcast(new SceneReceivedEventData { 
            Data = mapper.Map<ViewSceneDto>(message.Data)
        }, userIdentifier);
        
        await uow.CompleteAsync();
    }
}