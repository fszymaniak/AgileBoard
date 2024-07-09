using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal sealed class PostgresFinalEpicRepository : IFinalEpicRepository
{
    private readonly AgileBoardDbContext _dbContext;
    public PostgresFinalEpicRepository(AgileBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<FinalEpic?> GetFinalEpicAsync(EpicId? id) => _dbContext.Epics.OfType<FinalEpic>().SingleOrDefaultAsync(x => x.Id == id) ?? throw new EpicDoesNotExist();
    
    public async Task<IEnumerable<FinalEpic>> GetAllFinalEpicAsync() => await _dbContext.Epics.OfType<FinalEpic>().ToListAsync();

    public async Task AddFinalEpicAsync(FinalEpic epic)
    {
        await _dbContext.Epics.AddAsync(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateFinalEpicAsync(FinalEpic epic)
    {
        _dbContext.Update(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFinalEpicAsync(FinalEpic epic)
    {
        _dbContext.Epics.Remove(epic);
        await _dbContext.SaveChangesAsync();
    }
}