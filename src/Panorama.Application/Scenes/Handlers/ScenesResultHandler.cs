using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;

namespace Panorama.Scenes.Handlers;

public class ScenesResultHandler(
    ILogger<ScenesResultHandler> logger,
    IMapper mapper,
    IServiceProvider serviceProvider
    ) : IProcessMessageHandler<ScenesResultEto>
{
    public Task ProcessMessageAsync(ScenesResultEto request)
    {
        logger.LogTrace($"A request to process {nameof(ScenesResultEto)} was received. " +
                        $"Request {MediationActionEnum.Received.GetCode()}");
        
        if (request == null)
        {
            logger.LogWarning($"A request to process {nameof(ScenesResultEto)} was received as invalid." +
                              $"Request {MediationActionEnum.Ignored.GetCode()}");

            return; // Go no further.
        }
    }
}