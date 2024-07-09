using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Application.Services.FinalEpicsService;
using AgileBoard.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

[ApiController]
[Route("finalEpics")]
public sealed class FinalEpicsController : ControllerBase
{
    private readonly IFinalEpicsService _service;
    private readonly IClock _clock;

    public FinalEpicsController(IFinalEpicsService service, IClock clock)
    {
        _service = service;
        _clock = clock;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinalEpic>>> Get() => Ok(await _service.GetAllFinalEpicAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Epic>> Get(Guid id)
    {
        var epic = await _service.GetFinalEpicAsync(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost()]
    public async Task<ActionResult> Post(CreateFinalEpic command)
    {
        var id = await _service.CreateFinalEpicAsync(command with { Id = Guid.NewGuid(), Status = "New", CreatedDate = _clock.Current() });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }
    

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Epic>> Put(Guid id, UpdateFinalEpic command)
    {
        if (await _service.UpdateFinalEpicAsync(command with { Id = id }))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (await _service.DeleteFinalEpicAsync(new DeleteEpic(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
