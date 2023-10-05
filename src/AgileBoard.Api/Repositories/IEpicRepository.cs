using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;

namespace AgileBoard.Api.Repositories;

public interface IEpicRepository
{
    public Epic Get(Guid? id);

    public IEnumerable<Epic> GetAll();

    public void Add(Epic epic);
    
    public void Update(Epic epic, UpdateEpic command);

    public void Delete(Epic epic);
}
