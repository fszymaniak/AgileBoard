using AgileBoard.Application.Commands;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;

namespace AgileBoard.Application.Services.EpicsService;

public sealed class EpicsService : IEpicsService
{
    private readonly IEpicRepository _epicRepository;
    
    public EpicsService(IEpicRepository epicRepository)
    {
        _epicRepository = epicRepository;
    }

    public Epic Get(Guid? id) => _epicRepository.Get(id);

    public IEnumerable<Epic> GetAll() => _epicRepository.GetAll();
    
    public Guid? Create(CreateEpic command)
    {
        var epic = new Epic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        _epicRepository.Add(epic);

        return epic.Id;
    }

    public bool Update(UpdateEpic command)
    {
        var existingEpic = _epicRepository.Get(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        existingEpic.ChangeName(command.Name);
        existingEpic.ChangeStatus(command.Status);
        existingEpic.ChangeDescription(command.Description);
        existingEpic.ChangeAcceptanceCriteria(command.AcceptanceCriteria);
        
        _epicRepository.Update(existingEpic);
        return true;
    }

    public bool Delete(DeleteEpic command)
    {
        var existingEpic = Get(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        _epicRepository.Delete(existingEpic);
        return true;
    }
}