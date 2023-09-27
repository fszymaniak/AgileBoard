using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Services;

public sealed class EpicsService
{
    private static List<Epic> _epics = new();

    public Epic Get(Guid id) => GetAll().SingleOrDefault(e => e.Id == id);

    public IEnumerable<Epic> GetAll() => _epics;

    public Guid? Create(CreateEpic command)
    {
        var epic = new Epic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        _epics.Add(epic);

        return epic.Id;
    }

    public bool Update(UpdateEpic command)
    {
        var existingEpic = GetAll().SingleOrDefault(e => e.Id == command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        existingEpic.ChangeName(command.Name);
        existingEpic.ChangeStatus(command.Status);
        existingEpic.ChangeDescription(command.Description);
        existingEpic.ChangeAcceptanceCriteria(command.AcceptanceCriteria);

        return true;
    }

    public bool Delete(DeleteEpic command)
    {
        var existingEpic = Get(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        _epics.Remove(existingEpic);
        return true;
    }
}