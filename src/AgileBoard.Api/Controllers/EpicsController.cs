using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;
using AgileBoard.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

[ApiController]
[Route("epics")]
public sealed class EpicsController : ControllerBase
{
    private readonly EpicsService _service = new();

    [HttpGet]
    public ActionResult<IEnumerable<Epic>> Get() => Ok(_service.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<Epic> Get(Guid id)
    {
        var epic = _service.Get(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost]
    public ActionResult Post(CreateEpic command)
    {
        var id = _service.Create(command with { Id = Guid.NewGuid(), Status = "New", CreatedDate = DateTime.Now });
        
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<Epic> Put(Guid id, UpdateEpic command)
    {
        if (_service.Update(command with { EpicId = id }))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        if (_service.Delete(new DeleteEpic(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
