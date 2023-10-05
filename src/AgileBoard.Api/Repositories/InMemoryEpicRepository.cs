using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Repositories;

public class InMemoryEpicRepository : IEpicRepository
{
    private static readonly List<Epic> Epics = new();
    
    public Epic Get(Guid? id) => GetAll().SingleOrDefault(e => e.Id.Equals(id));
    
    public IEnumerable<Epic> GetAll() => Epics;

    public void Add(Epic epic) => Epics.Add(epic);

    public void Update(Epic existingEpic, UpdateEpic command)
    {
        existingEpic.ChangeName(command.Name);
        existingEpic.ChangeStatus(command.Status);
        existingEpic.ChangeDescription(command.Description);
        existingEpic.ChangeAcceptanceCriteria(command.AcceptanceCriteria);
    }

    public void Delete(Epic epic) => Epics.Remove(epic);
}
