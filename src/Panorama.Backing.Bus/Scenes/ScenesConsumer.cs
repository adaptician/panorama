using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto;
using Panorama.Scenes;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Backing.Bus.Scenes;

// Possible to consume multiple message types, but not sure if it will apply:
// https://masstransit.io/documentation/configuration#consume-multiple-message-types
public class ScenesConsumer(ILogger<ScenesRequestedXto> logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
    ) 
    : IConsumer<ScenesRequestedXto>
{
    public async Task Consume(ConsumeContext<ScenesRequestedXto> context)
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

        var carrier = sceneManager.CreateScenesReceivedCarrier();
        await carrier.Broadcast(new ScenesReceivedEventData { 
            Data = new PagedResultDto<ViewSceneDto>
            {
                TotalCount = message.Data.TotalCount,
                Items = mapper.Map<List<ViewSceneDto>>(message.Data.Items)
            }
        }, userIdentifier);
        
        await uow.CompleteAsync();

        await Task.CompletedTask;
    }
}