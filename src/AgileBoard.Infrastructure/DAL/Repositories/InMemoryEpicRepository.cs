using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryEpicRepository : IEpicRepository
{
    private static readonly List<Epic> Epics = new();
    
    public Task<Epic?> GetAsync(EpicId? id) => Task.FromResult(Epics.SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist());
    
    public Task<IEnumerable<Epic>> GetAllAsync() => Task.FromResult(Epics.AsEnumerable());

    public Task AddAsync(Epic epic)
    {
        Epics.Add(epic);
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Epic existingEpic)
    {
        var epicToUpdate = await GetAsync(existingEpic.Id);
        epicToUpdate.ChangeName(existingEpic.Name);
        epicToUpdate.ChangeStatus(existingEpic.Status);
        epicToUpdate.ChangeDescription(existingEpic.Description);
        epicToUpdate.ChangeAcceptanceCriteria(existingEpic.AcceptanceCriteria);
    }

    public Task DeleteAsync(Epic epic)
    {
        Epics.Remove(epic);
        return Task.CompletedTask;
    }
}
