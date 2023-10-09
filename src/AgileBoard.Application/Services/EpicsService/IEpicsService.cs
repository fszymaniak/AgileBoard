#nullable enable
using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;

namespace AgileBoard.Application.Services.EpicsService;

public interface IEpicsService
{
    public Epic Get(Guid? id);
    
    public IEnumerable<Epic> GetAll();

    public Guid? Create(CreateEpic command);

    public bool Update(UpdateEpic command);

    public bool Delete(DeleteEpic command);
}
