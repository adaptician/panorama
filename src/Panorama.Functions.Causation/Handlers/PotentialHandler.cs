using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Panorama.Communication.Shared.Causation;
using Panorama.Functions.Causation.Handlers.Contracts;

namespace Panorama.Functions.Causation.Handlers;

public class PotentialHandler<TCause>(ILogger<PotentialHandler<TCause>> logger, IMapper mapper) 
    : Handler<PotentialHandler<TCause>>(logger, mapper), 
        IRequestHandler<PotentialReceived<TCause>>
    where TCause : ICause
{
    public async Task Handle(PotentialReceived<TCause> request, CancellationToken cancellationToken)
    {
        logger.LogInformation("New potential has been received...");
        await Task.Delay(1000, cancellationToken);
        logger.LogInformation("... potential for something great!");
        await Task.Delay(1000, cancellationToken);
        logger.LogInformation("... for something truly magnificent!");
        logger.LogInformation($" with Id: {request.Cause.Id}");
    }
}