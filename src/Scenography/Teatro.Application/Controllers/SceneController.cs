using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teatro.Core.Scenography;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Bases.Dtos;
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
    [HttpPost]
    public async Task<IActionResult> Get(FilterInput input)
    {
        var records = await context.Scenes
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreationTime)
            .Skip(input.skipCount)
            .Take(input.ItemCount)
            .ToListAsync();
        var mapped = mapper.Map<List<ViewSceneDto>>(records);
        
        logger.LogInformation($"{nameof(Core.Scenes.Scene)}s retrieved. Skipped {input.skipCount} and Took {input.ItemCount}");
        return Ok(mapped);
    }
}