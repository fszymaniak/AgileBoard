using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Infrastructure.DAL.Repositories;

internal sealed class PostgresEpicRepository : IEpicRepository
{
    private readonly AgileBoardDbContext _dbContext;
    public PostgresEpicRepository(AgileBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Epic Get(EpicId? id) => _dbContext.Epics.SingleOrDefault(x => x.Id == id) ?? throw new EpicDoesNotExist();

    public IEnumerable<Epic> GetAll() => _dbContext.Epics.ToList();

    public void Add(Epic epic)
    {
        _dbContext.Epics.Add(epic);
        _dbContext.SaveChanges();
    }

    public void Update(Epic epic)
    {
        _dbContext.Update(epic);
        _dbContext.SaveChanges();
    }

    public void Delete(Epic epic)
    {
        _dbContext.Epics.Remove(epic);
        _dbContext.SaveChanges();
    }
}