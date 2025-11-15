using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryEpicRepository : IEpicRepository
{
    private readonly List<Epic> Epics = new();
    
    public Task<T?> GetEpicAsync<T>(EpicId? id) where T : Epic => Task.FromResult(Epics.OfType<T>().SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist())!;
    
    public Task<IEnumerable<Epic>> GetAllEpicAsync() => Task.FromResult(Epics.AsEnumerable());

    public Task AddEpicAsync(Epic epic)
    {
        Epics.Add(epic);
        return Task.CompletedTask;
    }

    public async Task UpdateEpicAsync(FinalEpic existingEpic)
    {
        var epicToUpdate = await GetEpicAsync<FinalEpic>(existingEpic.Id);
        
        epicToUpdate?.ChangeName(existingEpic.Name);
        epicToUpdate?.ChangeStatus(existingEpic.Status);
        epicToUpdate?.ChangeDescription(existingEpic.Description);
        epicToUpdate?.ChangeAcceptanceCriteria(existingEpic.AcceptanceCriteria);
    }
    
    public async Task UpdateEpicAsync(DraftEpic existingEpic)
    {
        var epicToUpdate = await GetEpicAsync<DraftEpic>(existingEpic.Id);

        epicToUpdate?.ChangeName(existingEpic.Name);
    }

    public Task DeleteEpicAsync(Epic epic)
    {
        Epics.Remove(epic);
        return Task.CompletedTask;
    }
}
