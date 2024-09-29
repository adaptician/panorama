using Abp;
using Abp.Application.Services.Dto;
using MassTransit;
using Microsoft.Extensions.Logging;
using Panorama.Backing.Bus.Shared.Scenes;
using Panorama.Backing.Dead.Shared.Scenes.Requests.Dto;
using Panorama.Scenes;
using Panorama.Scenes.Events.ScenesReceived;

namespace Panorama.Backing.Bus.Scenes;

// Possible to consume multiple message types, but not sure if it will apply:
// https://masstransit.io/documentation/configuration#consume-multiple-message-types
public class ScenesConsumer(ILogger<ScenesRequestedEto> logger,
    ISceneManager sceneManager
    ) 
    : IConsumer<ScenesRequestedEto>
{
    public async Task Consume(ConsumeContext<ScenesRequestedEto> context)
    {
        logger.LogInformation("Received scenes requested: {test}",
            context.Message.MaxResultCount
        );

        var carrier = sceneManager.CreateScenesReceivedCarrier();
        await carrier.Broadcast(new ScenesReceivedEventData { Data = new PagedResultDto<ViewSceneDto>
            {
                Items = [],
                TotalCount = 100
            }
        }, new UserIdentifier(1, 10005));

        await Task.CompletedTask;
    }
}