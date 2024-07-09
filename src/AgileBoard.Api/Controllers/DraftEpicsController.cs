using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Application.Services.DraftEpicsService;
using AgileBoard.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

[ApiController]
[Route("draftEpics")]
public sealed class DraftEpicsController : ControllerBase
{
    private readonly IDraftEpicsService _service;
    private readonly IClock _clock;

    public DraftEpicsController(IDraftEpicsService service, IClock clock)
    {
        _service = service;
        _clock = clock;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Epic>>> Get() => Ok(await _service.GetAllDraftEpicAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Epic>> Get(Guid id)
    {
        var epic = await _service.GetDraftEpicAsync(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(CreateDraftEpic command)
    {
        var id = await _service.CreateDraftEpicAsync(command with { Id = Guid.NewGuid(), CreatedDate = _clock.Current() });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Epic>> Put(Guid id, UpdateDraftEpic command)
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
        if (await _service.DeleteDraftEpicAsync(new DeleteEpic(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
