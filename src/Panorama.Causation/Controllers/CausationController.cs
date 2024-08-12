using Microsoft.AspNetCore.Mvc;

namespace Panorama.Causation.Controllers;

public class CausationController : ControllerBase
{
    private readonly ILogger<CausationController> _logger;

    public CausationController(ILogger<CausationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCausation")]
    public IResult Get()
    {
        return Results.Ok("retrieved one!");
    }
}