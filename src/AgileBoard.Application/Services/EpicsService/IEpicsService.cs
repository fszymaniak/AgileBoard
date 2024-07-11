#nullable enable
using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;

namespace AgileBoard.Application.Services.EpicsService;

public interface IEpicsService
{
    Task<T?> GetEpicAsync<T>(Guid? id) where T : Epic;
    
    Task<IEnumerable<Epic>> GetAllEpicAsync();
    
    Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command);
    
    Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command);
    
    Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command);
    
    Task<bool> UpdateDraftEpicAsync(UpdateFinalEpic command);
    
    Task<bool> DeleteEpicAsync(DeleteEpic command);
}
