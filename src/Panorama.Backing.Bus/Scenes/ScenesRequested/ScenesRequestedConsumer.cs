using Abp.Domain.Uow;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Panorama.Authorization.Users;
using Panorama.Backing.Bus.Shared.Common.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Dto;
using Panorama.Backing.Bus.Shared.Scenes.Xto.RequestScenes;
using Panorama.Scenes;
using Panorama.Scenes.Events.ScenesReceived;
using Castle.Core.Logging;
using Panorama.Events.Errors;

namespace Panorama.Backing.Bus.Scenes.ScenesRequested;

public class ScenesRequestedConsumer(ILogger logger,
    IServiceProvider serviceProvider,
    IMapper mapper,
    UserManager userManager,
    ISceneManager sceneManager
    ) 
    : IConsumer<ScenesRequestedXto>
{
    public async Task Consume(ConsumeContext<ScenesRequestedXto> context)
    {
        logger.Info($"Received scenes requested: {context.MessageId}");

        if (context.Message is null)
        {
            throw new Exception($"Unable to consume message - payload was invalid. " +
                                $"Message: {context.Message}");
        }
        
        var message = context.Message;

        using var scope = serviceProvider.CreateScope();
        var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();
        
        try
        {
            var userIdentifier = await userManager.GetUserIdentifierByCorrelationIdAsync(message.UserCorrelationId);
            
            var carrier = sceneManager.CreateScenesReceivedCarrier();
            await carrier.Broadcast(new ScenesReceivedEventData { 
                Data = new PagedResultDto<ViewSceneDto>
                {
                    TotalCount = message.Data.TotalCount,
                    Items = mapper.Map<List<ViewSceneDto>>(message.Data.Items)
                }
            }, userIdentifier);
        }
        catch (Exception e)
        {
            var errorMessage = $"Failed to consume result for {nameof(ScenesRequestedXto)}";
            logger.Error(errorMessage, e);
            
            var carrier = sceneManager.CreateErroredCarrier();
            await carrier.Broadcast(new ErroredEventData
            {
                Error = new ErrorDto
                {
                    ErrorMessage = errorMessage
                }
            });
        }
        finally
        {
            await uow.CompleteAsync();
        }
    }
}