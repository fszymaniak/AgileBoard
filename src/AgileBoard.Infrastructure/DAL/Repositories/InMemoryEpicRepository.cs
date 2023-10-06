using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryEpicRepository : IEpicRepository
{
    private static readonly List<Epic> Epics = new();
    
    public Epic Get(Guid? id) => GetAll().SingleOrDefault(e => e.Id.Equals(id));
    
    public IEnumerable<Epic> GetAll() => Epics;

    public void Add(Epic epic) => Epics.Add(epic);

    public void Update(Epic existingEpic, string name, string status, string description, string acceptanceCriteria)
    {
        existingEpic.ChangeName(name);
        existingEpic.ChangeStatus(status);
        existingEpic.ChangeDescription(description);
        existingEpic.ChangeAcceptanceCriteria(acceptanceCriteria);
    }

    public void Delete(Epic epic) => Epics.Remove(epic);
}
