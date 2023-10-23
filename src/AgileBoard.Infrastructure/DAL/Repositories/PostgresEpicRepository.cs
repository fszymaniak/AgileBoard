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

    public Task<Epic?> GetAsync(EpicId? id) => _dbContext.Epics.SingleOrDefaultAsync(x => x.Id == id) ?? throw new EpicDoesNotExist();

    public async Task<IEnumerable<Epic>> GetAllAsync() => await _dbContext.Epics.ToListAsync();

    public async Task AddAsync(Epic epic)
    {
        await _dbContext.Epics.AddAsync(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Epic epic)
    {
        _dbContext.Update(epic);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Epic epic)
    {
        _dbContext.Epics.Remove(epic);
        await _dbContext.SaveChangesAsync();
    }
}