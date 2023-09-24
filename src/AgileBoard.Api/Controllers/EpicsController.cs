using AgileBoard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgileBoard.Api.Controllers;

[ApiController]
[Route("epics")]
public sealed class EpicsController : ControllerBase
{
    private static int _id = 1;
    private static readonly List<Epic> Epics = new();

    [HttpGet]
    public ActionResult<IEnumerable<Epic>> Get() => Ok(Epics);

    [HttpGet("{id:int}")]
    public ActionResult<Epic> Get(int id)
    {
        var epic = Epics.SingleOrDefault(e => e.Id == id);
        if (epic is null)
        {
            return NotFound();
        }

        return Ok(epic);
    }

    [HttpPost]
    public ActionResult Post(Epic epic)
    {
        var epicNameAlreadyExists = Epics.Any(e => e.Name == epic.Name);
        
        if (epicNameAlreadyExists)
        {
            return BadRequest();
        }
        
        epic.Id = _id;
        epic.Status = "New";
        epic.CreatedDate = DateTime.UtcNow;
        _id++;
        
        Epics.Add(epic);

        return CreatedAtAction(nameof(Get), new { id = epic.Id }, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Epic> Put(int id, Epic epic)
    {
        var existingEpic = Epics.SingleOrDefault(e => e.Id == id);
        
        if (existingEpic is null)
        {
            return NotFound();
        }

        existingEpic.Name = epic.Name;
        existingEpic.Status = epic.Status;
        existingEpic.Description = epic.Description;
        existingEpic.AcceptanceCriteria = epic.AcceptanceCriteria;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var existingEpic = Epics.SingleOrDefault(e => e.Id == id);
        
        if (existingEpic is null)
        {
            return NotFound();
        }

        Epics.Remove(existingEpic);
        return NoContent();
    }
}
