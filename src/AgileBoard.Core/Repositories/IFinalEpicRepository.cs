#nullable enable
using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Repositories;

public interface IFinalEpicRepository
{
    Task<FinalEpic?> GetFinalEpicAsync(EpicId? id);

    Task<IEnumerable<FinalEpic>> GetAllFinalEpicAsync();

    Task AddFinalEpicAsync(FinalEpic epic);
    
    Task UpdateFinalEpicAsync(FinalEpic existingEpic);
    
    Task DeleteFinalEpicAsync(FinalEpic epic);
}
