using AgileBoard.Api.Models;
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

    [HttpGet("{id:int}")]
    public ActionResult<Epic> Get(int id)
    {
        var epic = _service.Get(id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost]
    public ActionResult Post(Epic epic)
    {
        var id = _service.Create(epic);
        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Epic> Put(int id, Epic epic)
    {
        epic.Id = id;
        if (_service.Update(epic))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        if (_service.Delete(id))
        {
            return NoContent();
        }

        return NotFound();
    }
}
