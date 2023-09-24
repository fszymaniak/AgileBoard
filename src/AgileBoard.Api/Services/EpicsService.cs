using AgileBoard.Api.Models;

namespace AgileBoard.Api.Services;

public sealed class EpicsService
{
    private static int _id = 1;
    private static readonly List<Epic> Epics = new();

    public Epic Get(int id) => Epics.SingleOrDefault(e => e.Id == id);

    public IEnumerable<Epic> GetAll() => Epics;

    public int? Create(Epic epic)
    {
        var epicNameAlreadyExists = Epics.Any(e => e.Name == epic.Name);
        
        if (epicNameAlreadyExists)
        {
            return default;
        }
        
        epic.Id = _id;
        epic.Status = "New";
        epic.CreatedDate = DateTime.UtcNow;
        _id++;
        
        Epics.Add(epic);

        return epic.Id;
    }

    public bool Update(Epic epic)
    {
        var existingEpic = Epics.SingleOrDefault(e => e.Id == epic.Id);
        
        if (existingEpic is null)
        {
            return false;
        }

        existingEpic.Name = epic.Name;
        existingEpic.Status = epic.Status;
        existingEpic.Description = epic.Description;
        existingEpic.AcceptanceCriteria = epic.AcceptanceCriteria;

        return true;
    }

    public bool Delete(int id)
    {
        var existingEpic = Epics.SingleOrDefault(e => e.Id == id);
        
        if (existingEpic is null)
        {
            return false;
        }

        Epics.Remove(existingEpic);
        return true;
    }
}