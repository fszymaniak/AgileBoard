using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal sealed class PostgresDraftEpicRepository : IDraftEpicRepository
{
    private readonly AgileBoardDbContext _dbContext;
    public PostgresDraftEpicRepository(AgileBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<DraftEpic?> GetDraftEpicAsync(EpicId? id) => _dbContext.Epics.OfType<DraftEpic>().SingleOrDefaultAsync(x => x.Id == id) ?? throw new EpicDoesNotExist();
    
    public async Task<IEnumerable<DraftEpic>> GetAllDraftEpicAsync() => await _dbContext.Epics.OfType<DraftEpic>().ToListAsync();

    public async Task AddDraftEpicAsync(DraftEpic epic)
    {
        await _dbContext.Epics.AddAsync(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDraftEpicAsync(DraftEpic epic)
    {
        _dbContext.Update(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDraftEpicAsync(DraftEpic epic)
    {
        _dbContext.Epics.Remove(epic);
        await _dbContext.SaveChangesAsync();
    }
}