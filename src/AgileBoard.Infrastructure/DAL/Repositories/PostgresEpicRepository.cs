using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal sealed class PostgresEpicRepository : IEpicRepository
{
    private readonly AgileBoardDbContext _dbContext;
    public PostgresEpicRepository(AgileBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<T?> GetEpicAsync<T>(EpicId? id) where T : Epic => Task.FromResult(_dbContext.Epics.OfType<T>().SingleOrDefault(e => e.Id.Equals(id)) ?? throw new EpicDoesNotExist())!;
    
    public async Task<IEnumerable<Epic>> GetAllEpicAsync() => await _dbContext.Epics.OfType<Epic>().ToListAsync();

    public async Task AddEpicAsync(Epic epic)
    {
        await _dbContext.Epics.AddAsync(epic);
        await _dbContext.SaveChangesAsync();
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

    public async Task DeleteEpicAsync(Epic epic)
    {
        _dbContext.Epics.Remove(epic);
        await _dbContext.SaveChangesAsync();
    }
}