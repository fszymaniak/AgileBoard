#nullable enable
using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;

namespace AgileBoard.Application.Services.EpicsService;

public interface IEpicsService
{
    Task<Epic> GetAsync(Guid? id);
    
    Task<IEnumerable<Epic>> GetAllAsync();

    Task<Guid?> CreateAsync(CreateEpic command);

    Task<bool> UpdateAsync(UpdateEpic command);

    Task<bool> DeleteAsync(DeleteEpic command);
}
