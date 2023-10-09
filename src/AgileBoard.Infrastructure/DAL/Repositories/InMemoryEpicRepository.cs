using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryEpicRepository : IEpicRepository
{
    private static readonly List<Epic> Epics = new();
    
    public Epic Get(EpicId? id) => GetAll().SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist();
    
    public IEnumerable<Epic> GetAll() => Epics;

    public void Add(Epic epic) => Epics.Add(epic);

    public void Update(Epic existingEpic)
    {
        var epicToUpdate = Get(existingEpic.Id);
        epicToUpdate.ChangeName(existingEpic.Name);
        epicToUpdate.ChangeStatus(existingEpic.Status);
        epicToUpdate.ChangeDescription(existingEpic.Description);
        epicToUpdate.ChangeAcceptanceCriteria(existingEpic.AcceptanceCriteria);
    }

    public void Delete(Epic epic) => Epics.Remove(epic);
}
