using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Panorama.Backing.Producers;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;
using Panorama.Common.Repositories;
using Teatro.Core.Scenes;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Bases.Etos;
using Teatro.Shared.Scenes.Etos;

namespace Teatro.Application.Handlers;

public class ScenesRequestedHandler(
    ILogger<ScenesRequestedHandler> logger,
    IMapper mapper,
    // TeatroDbContext context,
    // IQueryableRepository<Scene, long> sceneRepository,
    ScenesProducer producer,
    IServiceProvider serviceProvider
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
        
        // A hosted service does not manage scope in the same way as web-related services.
        // Some services will need to be manually scoped at the appropriate time.
        using var scope = serviceProvider.CreateScope();
                
        var sceneRepository = scope.ServiceProvider.GetRequiredService<IQueryableRepository<Scene, long>>();
        
        // var query = context.Scenes
        //     .Where(x => !x.IsDeleted)
        //     .OrderByDescending(x => x.CreationTime);
        var query = sceneRepository.GetAll();
        
        var totalCount = await query.CountAsync();
        var records = await query
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount)
            .ToListAsync();
        
        // logger.LogInformation($"{nameof(context.Scenes)} retrieved. " +
        //                       $"Skipped {request.SkipCount} and Took {request.MaxResultCount}");
        
        var resultEto = new PagedResultEto<ViewSceneEto>
        {
            Items = mapper.Map<List<ViewSceneEto>>(records),
            TotalCount = totalCount
        };
        
        producer.PublishMessage(resultEto, RoutingKeys.GetAllResult);
        
        logger.LogTrace($"A request to retrieve Scenes was published. " +
                     $"Request {MediationActionEnum.Published.GetCode()}");
    }
}