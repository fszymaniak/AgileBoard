#nullable enable
using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;

namespace AgileBoard.Application.Services.DraftEpicsService;

public interface IDraftEpicsService
{
    Task<DraftEpic?> GetDraftEpicAsync(Guid? id);
    
    Task<IEnumerable<DraftEpic>> GetAllDraftEpicAsync();

    Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command);

    Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command);

    Task<bool> DeleteDraftEpicAsync(DeleteEpic command);
}
