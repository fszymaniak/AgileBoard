using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Application.Services.EpicsService;
using AgileBoard.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

/// <summary>
/// Epics controller - handles epic creation, updates, and retrieval.
///
/// AUTHENTICATION & AUTHORIZATION:
/// When authentication is enabled, the user's JobTitle must be set in the JWT token claims.
/// The claim can be named either "JobTitle" or "role".
///
/// Valid JobTitle values:
/// - BusinessAnalyst
/// - ProductOwner
/// - ScrumMaster
/// - DevelopmentTeamMember
///
/// Example JWT claim configuration:
/// new Claim("JobTitle", "ProductOwner")
/// or
/// new Claim("role", "DevelopmentTeamMember")
///
/// Different roles have different permissions as defined by the policy classes:
/// - BusinessAnalyst & ProductOwner: Can create/update both FinalEpic and DraftEpic
/// - ScrumMaster: Can create/update DraftEpic only
/// - DevelopmentTeamMember: Can create/update DraftEpic only
/// </summary>
[ApiController]
[Route("epics")]
public sealed class EpicsController : ControllerBase
{
    private readonly IEpicsService _service;
    private readonly IClock _clock;

    public EpicsController(IEpicsService service, IClock clock)
    {
        _service = service;
        _clock = clock;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinalEpic>>> Get() => Ok(await _service.GetAllEpicAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Epic>> Get(Guid id)
    {
        var epic = await _service.GetEpicAsync<Epic>(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost("final")]
    public async Task<ActionResult> Post(CreateFinalEpic command)
    {
        var id = await _service.CreateFinalEpicAsync(command with { Id = Guid.NewGuid(), Status = "New", CreatedDate = _clock.Current() });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }
    
    [HttpPost("draft")]
    public async Task<ActionResult> Post(CreateDraftEpic command)
    {
        var id = await _service.CreateDraftEpicAsync(command with { Id = Guid.NewGuid(), Name = command.Name, CreatedDate = _clock.Current() });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }
    

    [HttpPut("final/{id:guid}")]
    public async Task<ActionResult<FinalEpic>> Put(Guid id, UpdateFinalEpic command)
    {
        if (await _service.UpdateFinalEpicAsync(command with { Id = id }))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpPut("draft/{id:guid}")]
    public async Task<ActionResult<DraftEpic>> Put(Guid id, UpdateDraftEpic command)
    {
        if (await _service.UpdateDraftEpicAsync(command with { Id = id }))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (await _service.DeleteEpicAsync(new DeleteEpic(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
