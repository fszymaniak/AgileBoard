using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Services.EpicsService;

public interface IEpicsService
{
    public Epic Get(Guid? id);
    
    public IEnumerable<Epic> GetAll();

    public Guid? Create(CreateEpic command);

    public bool Update(UpdateEpic command);

    public bool Delete(DeleteEpic command);
}
