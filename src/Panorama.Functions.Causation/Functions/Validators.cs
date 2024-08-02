using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Panorama.Functions.Causation.Handlers.Contracts;
using Panorama.Functions.Causation.Requests;

namespace Panorama.Functions.Causation.Functions;

public class Validators
{
    private readonly ILogger<Validators> _logger;
    private readonly IMediator _mediatr;

    public Validators(ILogger<Validators> logger, 
        IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }
    
    [Function(nameof(SubmitCause))]
    public async Task<IActionResult> SubmitCause(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "submit")]
        HttpRequestData req,
        FunctionContext context)
    {
        try
        {
            var rawJson = await req.ReadAsStringAsync();
            if (string.IsNullOrEmpty(rawJson))
            {
                throw new ArgumentException("Unable to process post - body is invalid.");
            }
            
            var payload = JsonConvert.DeserializeObject<Cause>(rawJson);
            if (payload == null || string.IsNullOrEmpty(payload.Id))
            {
                throw new ArgumentException("Unable to process post - body could not be deserialized.");
            }
            
            _logger.LogInformation("A valid cause posting was received.");
            
            var notification = new PotentialReceived<Cause>
            {
                Cause = payload
            };
            await _mediatr.Send(notification, context.CancellationToken);
            
            return await Task.FromResult<IActionResult>(new OkObjectResult("received"));
        }
        catch (Exception e)
        {
            var message = $"A cause posting failed {e}";
            _logger.LogError(message);
            return await Task.FromResult<IActionResult>(new BadRequestObjectResult("invalid"));
        }
    }
}