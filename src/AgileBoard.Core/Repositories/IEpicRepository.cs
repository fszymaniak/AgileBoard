#nullable enable
using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Core.Repositories;

public interface IEpicRepository
{
    public Epic Get(EpicId? id);

    public IEnumerable<Epic> GetAll();

    public void Add(Epic epic);

    public void Update(Epic existingEpic);

    public void Delete(Epic epic);
}
