using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teatro.Core.Scenes;
using Teatro.Core.Scenography;
using Teatro.EntityFrameworkCore;
using Teatro.Shared.Scenes.Dtos;
using Teatro.Shared.Bases.Dtos;

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
    public async Task<IActionResult> Get([FromQuery] int maxResultCount, [FromQuery] int skipCount)
    {
        var query = context.Scenes
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreationTime);

        var totalCount = await query.CountAsync();
        var records = await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
        
        logger.LogInformation($"{nameof(Scene)}s retrieved. Skipped {skipCount} and Took {maxResultCount}");
        
        return Ok(new PagedResultDto<ViewSceneDto>
        {
            Items = mapper.Map<List<ViewSceneDto>>(records),
            TotalCount = totalCount
        });
    }
    
    // GET: api/scenes/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var record = await context.Scenes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        
        if (record == null)
        {
            logger.LogInformation($"{nameof(Scene)} Id: {id} not found");
            return NotFound();
        }
        
        var mapped = mapper.Map<ViewSceneDto>(record);
        
        logger.LogInformation($"{nameof(Scene)} Id: {id} retrieved");
        return Ok(mapped);
    }
    
    // POST: api/scenes
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSceneDto input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        var record = mapper.Map<Scene>(input);
        
        var scenography = new Scenography();
        var document = new ScenographyDocument(scenography.DocumentId, input.InitialSceneData);
        await scenographyDocumentManager.CreateAsync(document);

        record.Scenography = scenography;    
        var persisted = await context.Scenes.AddAsync(record);
        await context.SaveChangesAsync();
        logger.LogInformation($"{nameof(Scene)} created.");
        
        var mapped = mapper.Map<ViewSceneDto>(persisted.Entity);
        return CreatedAtAction(nameof(Create), new { id = record.Id }, mapped);
    }
    
    // PUT: api/scenes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateSceneDto input)
    {
        var existingRecord = await context.Scenes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (existingRecord == null)
        {
            return NotFound();
        }
        
        var updatedRecord = mapper.Map(input, existingRecord);
        context.Scenes.Update(updatedRecord);
        
        await context.SaveChangesAsync();
        logger.LogInformation($"{nameof(Scene)} Id: {id} updated.");
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var existingRecord = await context.Scenes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (existingRecord == null)
        {
            return NotFound();
        }

        existingRecord.IsDeleted = true;
        context.Update(existingRecord);
        
        await context.SaveChangesAsync();
        logger.LogInformation($"{nameof(Scene)} Id: {id} deleted.");
        return NoContent();
    }
}