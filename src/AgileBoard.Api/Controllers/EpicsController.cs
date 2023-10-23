using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Application.Services.EpicsService;
using AgileBoard.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<Epic>>> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Epic>> Get(Guid id)
    {
        var epic = await _service.GetAsync(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateEpic command)
    {
        var id = await _service.CreateAsync(command with { Id = Guid.NewGuid(), Status = "New", CreatedDate = _clock.Current() });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Epic>> Put(Guid id, UpdateEpic command)
    {
        if (await _service.UpdateAsync(command with { Id = id }))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (await _service.DeleteAsync(new DeleteEpic(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
