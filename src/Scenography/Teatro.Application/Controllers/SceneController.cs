using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teatro.Core.Scenography;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Scenes.Dtos;

namespace Teatro.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class SceneController(
    ILogger<SceneController> logger,
    IMapper mapper,
    TeatroDbContext context,
    ScenographyDocumentManager scenographyDocumentManager)
    : ControllerBase
{
    // GET: api/scenes
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int itemCount, [FromQuery] int skipCount)
    {
        var records = await context.Scenes
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreationTime)
            .Skip(skipCount)
            .Take(itemCount)
            .ToListAsync();
        var mapped = mapper.Map<List<ViewSceneDto>>(records);
        
        logger.LogInformation($"{nameof(Core.Scenes.Scene)}s retrieved. Skipped {skipCount} and Took {itemCount}");
        return Ok(mapped);
    }
    
    // GET: api/scenes/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var record = await context.Scenes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        
        if (record == null)
        {
            logger.LogInformation($"{nameof(Core.Scenes.Scene)} Id: {id} not found");
            return NotFound();
        }
        
        var mapped = mapper.Map<ViewSceneDto>(record);
        
        logger.LogInformation($"{nameof(Core.Scenes.Scene)} Id: {id} retrieved");
        return Ok(mapped);
    }
}