using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Panorama.Backing.Producers;
using Panorama.Backing.Shared.Messages;
using Panorama.Backing.Shared.RoutingKeys;
using Panorama.Backing.Shared.Scenes.Requests.Eto;
using Panorama.Common.Constants;
using Panorama.Common.Extensions;
using Panorama.Common.Mediations;
using Panorama.Common.Repositories;
using Teatro.Core.Scenes;
using Teatro.Shared.Bases.Etos;
using Teatro.Shared.Scenes.Etos;

namespace Teatro.Application.Handlers;

public class ScenesOperationHandler(
    ILogger<ScenesOperationHandler> logger,
    IMapper mapper,
    ScenesProducer producer,
    IServiceProvider serviceProvider
    ) : IProcessMessageHandler<ScenesOperationEto>
{
    public async Task ProcessMessageAsync(ScenesOperationEto request)
    {
        logger.LogTrace($"A request to process {nameof(ScenesOperationEto)} was received. " +
                     $"Request {MediationActionEnum.Received.GetCode()}");
        
        if (request == null)
        {
            logger.LogWarning($"A request to process {nameof(ScenesOperationEto)} was received as invalid." +
                        $"Request {MediationActionEnum.Ignored.GetCode()}");

            return; // Go no further.
        }

        switch (request.Operation)
        {
            case BrokerMessageOperations.GetAll:
            {
                await ProcessGetAll(request.Data);
                break;
            }
            default:
                throw new Exception("Unable to process message - operation unrecognized.");
        }
        
        
    }

    private async Task ProcessGetAll(string messageData)
    {
        var request = JsonConvert.DeserializeObject<ScenesRequestedEto>(messageData);
        
        if (request == null)
        {

            throw new Exception($"A request to retrieve Scenes was received as invalid." +
                                $"Request {MediationActionEnum.Rejected.GetCode()}");
        }
        
        // A hosted service does not manage scope in the same way as web-related services.
        // Some services will need to be manually scoped at the appropriate time.
        using var scope = serviceProvider.CreateScope();
                
        var sceneRepository = scope.ServiceProvider.GetRequiredService<IQueryableRepository<Scene, long>>();
        
        var query = sceneRepository.GetAll();
        
        var totalCount = await query.CountAsync();
        var records = await query
            .OrderBy(x => x.CreationTime)
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount)
            .ToListAsync();
        
        logger.LogInformation($"{nameof(Scene)} retrieved. " +
                              $"Skipped {request.SkipCount} and Took {request.MaxResultCount}");
        
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