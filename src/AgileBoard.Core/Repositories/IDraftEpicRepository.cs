#nullable enable
using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Repositories;

public interface IDraftEpicRepository
{
    Task<DraftEpic?> GetDraftEpicAsync(EpicId? id);

    Task<IEnumerable<DraftEpic>> GetAllDraftEpicAsync();

    Task AddDraftEpicAsync(DraftEpic epic);
    
    Task UpdateDraftEpicAsync(DraftEpic existingEpic);
    
    Task DeleteDraftEpicAsync(DraftEpic epic);
}
