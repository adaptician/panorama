using Microsoft.AspNetCore.Mvc;

namespace Panorama.Causation.Controllers;

public class CausalityController : ControllerBase
{
    private readonly ILogger<CausalityController> _logger;

    public CausalityController(ILogger<CausalityController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCausation")]
    public IResult Get()
    {
        return Results.Ok("retrieved one!");
    }
    
    // TODO: still needed? https://www.c-sharpcorner.com/article/rabbitmq-message-queue-using-net-core-6-web-api/
}