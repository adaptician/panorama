using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Panorama.Functions.Causation.Handlers;

public abstract class Handler<T>(ILogger<T> logger, IMapper mapper)
{
    protected readonly ILogger<T> Logger = logger;
    protected readonly IMapper Mapper = mapper;
}