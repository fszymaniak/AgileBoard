#nullable enable
using AgileBoard.Core.Entities;

namespace AgileBoard.Core.Repositories;

public interface IEpicRepository
{
    public Epic? Get(Guid? id);

    public IEnumerable<Epic> GetAll();

    public void Add(Epic epic);
    
    public void Update(Epic epic, string name, string status, string description, string acceptanceCriteria);

    public void Delete(Epic epic);
}
