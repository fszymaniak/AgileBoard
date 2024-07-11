#nullable enable
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Repositories;

public interface IEpicRepository
{
    Task<T?> GetEpicAsync<T>(EpicId? id) where T : Epic;

    Task<IEnumerable<Epic>> GetAllEpicAsync();

    Task AddEpicAsync(Epic epic);
    
    Task UpdateEpicAsync(FinalEpic existingEpic);
    
    Task UpdateEpicAsync(DraftEpic existingEpic);
    
    Task DeleteEpicAsync(Epic epic);
}
