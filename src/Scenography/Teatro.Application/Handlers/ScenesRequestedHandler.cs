using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Panorama.Backing.Producers;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Bases.Etos;
using Teatro.Shared.Scenes.Etos;

namespace Teatro.Application.Handlers;

public class ScenesRequestedHandler(
    ILogger<ScenesRequestedHandler> logger,
    IMapper mapper,
    // TeatroDbContext context,
    ScenesProducer producer
    ) : IProcessMessageHandler<ScenesRequestedEto>
{
    public async Task ProcessMessageAsync(ScenesRequestedEto request)
    {
        logger.LogTrace($"A request to retrieve Scenes was received. " +
                     $"Request {MediationActionEnum.Received.GetCode()}");
        
        if (request == null)
        {
            logger.LogWarning($"A request to retrieve Scenes was received as invalid." +
                        $"Request {MediationActionEnum.Ignored.GetCode()}");

            return; // Go no further.
        }
        
        // var query = context.Scenes
        //     .Where(x => !x.IsDeleted)
        //     .OrderByDescending(x => x.CreationTime);
        //
        // var totalCount = await query.CountAsync();
        // var records = await query
        //     .Skip(request.SkipCount)
        //     .Take(request.MaxResultCount)
        //     .ToListAsync();
        //
        // logger.LogInformation($"{nameof(context.Scenes)} retrieved. " +
        //                       $"Skipped {request.SkipCount} and Took {request.MaxResultCount}");
        //
        // var resultEto = new PagedResultEto<ViewSceneEto>
        // {
        //     Items = mapper.Map<List<ViewSceneEto>>(records),
        //     TotalCount = totalCount
        // };

        var resultEto = new ViewSceneEto(); // TODO: fix
        
        producer.PublishMessage(resultEto, RoutingKeys.GetAllResult);
        
        logger.LogTrace($"A request to retrieve Scenes was published. " +
                     $"Request {MediationActionEnum.Published.GetCode()}");
    }
}