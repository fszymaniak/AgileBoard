#nullable enable
using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;

namespace AgileBoard.Application.Services.FinalEpicsService;

public interface IFinalEpicsService
{
    Task<FinalEpic?> GetFinalEpicAsync(Guid? id);
    
    Task<IEnumerable<FinalEpic>> GetAllFinalEpicAsync();
    
    Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command);
    
    Task<bool> UpdateFinalEpicAsync(UpdateFinalEpic command);
    
    Task<bool> DeleteFinalEpicAsync(DeleteEpic command);
}
