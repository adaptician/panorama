using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Scenes;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Dto;
using Panorama.Scenes;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Backing.Bus.Scenes;

// Possible to consume multiple message types, but not sure if it will apply:
// https://masstransit.io/documentation/configuration#consume-multiple-message-types
public class ScenesConsumer(ILogger<ScenesRequestedEto> logger,
    IServiceProvider serviceProvider,
    UserManager userManager,
    ISceneManager sceneManager
    ) 
    : IConsumer<ScenesRequestedEto>
{
    public async Task Consume(ConsumeContext<ScenesRequestedEto> context)
    {
        logger.LogInformation("Received scenes requested: {test}",
            context.Message.MaxResultCount
        );

        using var scope = serviceProvider.CreateScope();
        var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();
        
        var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(context.Message.UserCorrelationId);

        var carrier = sceneManager.CreateScenesReceivedCarrier();
        await carrier.Broadcast(new ScenesReceivedEventData { Data = new PagedResultDto<ViewSceneDto>
            {
                Items = [],
                TotalCount = 500
            }
        }, userIdentifier);
        
        await uow.CompleteAsync();

        await Task.CompletedTask;
    }
}