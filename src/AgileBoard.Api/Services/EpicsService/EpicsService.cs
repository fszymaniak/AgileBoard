using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Services.EpicsService;

public sealed class EpicsService : IEpicsService
{
    private static readonly List<Epic> Epics = new();

    public Epic Get(Guid? id) => GetAll().SingleOrDefault(e => e.Id.Equals(id));

    public IEnumerable<Epic> GetAll() => Epics;
    
    public Guid? Create(CreateEpic command)
    {
        var epic = new Epic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        Epics.Add(epic);

        return epic.Id;
    }

    public bool Update(UpdateEpic command)
    {
        var existingEpic = GetAll().SingleOrDefault(e => e.Id.Equals(command.Id));
        
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

        Epics.Remove(existingEpic);
        return true;
    }
}