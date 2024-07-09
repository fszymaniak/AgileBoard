using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryDraftEpicRepository : IDraftEpicRepository
{
    private static readonly List<DraftEpic> DraftEpics = new();
    
    public Task<DraftEpic?> GetDraftEpicAsync(EpicId? id) => Task.FromResult(DraftEpics.SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist())!;

    public Task<IEnumerable<DraftEpic>> GetAllDraftEpicAsync() => Task.FromResult(DraftEpics.AsEnumerable());

    public Task AddDraftEpicAsync(DraftEpic epic)
    {
        DraftEpics.Add(epic);
        return Task.CompletedTask;
    }

    public async Task UpdateDraftEpicAsync(DraftEpic existingEpic)
    {
        var draftEpicToUpdate = await GetDraftEpicAsync(existingEpic.Id);
        draftEpicToUpdate?.ChangeName(existingEpic.Name);
    }

    public Task DeleteDraftEpicAsync(DraftEpic epic)
    {
        DraftEpics.Remove(epic);
        return Task.CompletedTask;
    }
}
