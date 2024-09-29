using MassTransit;
using Microsoft.Extensions.Logging;
using Panorama.Backing.Bus.Shared.Scenes;

namespace Panorama.Backing.Bus.Scenes;

// Possible to consume multiple message types, but not sure if it will apply:
// https://masstransit.io/documentation/configuration#consume-multiple-message-types
public class ScenesConsumer : IConsumer<ScenesRequestedEto>
{
    private readonly ILogger<ScenesRequestedEto> _logger;

    public ScenesConsumer(ILogger<ScenesRequestedEto> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ScenesRequestedEto> context)
    {
        _logger.LogInformation("Received scenes requested: {test}",
            context.Message.MaxResultCount
        );

        await Task.CompletedTask;
    }
}