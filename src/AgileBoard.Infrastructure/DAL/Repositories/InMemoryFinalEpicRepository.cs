using System.Runtime.CompilerServices;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal class InMemoryFinalEpicRepository : IFinalEpicRepository
{
    private static readonly List<FinalEpic> FinalEpics = new();
    
    public Task<FinalEpic?> GetFinalEpicAsync(EpicId? id) => Task.FromResult(FinalEpics.SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist())!;
    
    public Task<IEnumerable<FinalEpic>> GetAllFinalEpicAsync() => Task.FromResult(FinalEpics.AsEnumerable());

    public Task AddFinalEpicAsync(FinalEpic epic)
    {
        FinalEpics.Add(epic);
        return Task.CompletedTask;
    }

    public async Task UpdateFinalEpicAsync(FinalEpic existingEpic)
    {
        var epicToUpdate = await GetFinalEpicAsync(existingEpic.Id);
        epicToUpdate?.ChangeName(existingEpic.Name);
        epicToUpdate?.ChangeStatus(existingEpic.Status);
        epicToUpdate?.ChangeDescription(existingEpic.Description);
        epicToUpdate?.ChangeAcceptanceCriteria(existingEpic.AcceptanceCriteria);
    }

    public Task DeleteFinalEpicAsync(FinalEpic epic)
    {
        FinalEpics.Remove(epic);
        return Task.CompletedTask;
    }
}
