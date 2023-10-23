#nullable enable
using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Repositories;

public interface IEpicRepository
{
    Task<Epic?> GetAsync(EpicId? id);

    Task<IEnumerable<Epic>> GetAllAsync();

    Task AddAsync(Epic epic);

    Task UpdateAsync(Epic existingEpic);

    Task DeleteAsync(Epic epic);
}
