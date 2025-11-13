using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.UserContext;
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.DomainServices.Update;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Application.Services.EpicsService;

public sealed class EpicsService : IEpicsService
{
    private readonly IEpicRepository _epicRepository;
    private readonly IEpicCreationService _epicCreationService;
    private readonly IEpicUpdateService _epicUpdateService;
    private readonly IUserContext _userContext;

    public EpicsService(IEpicRepository epicRepository, IEpicCreationService epicCreationService, IEpicUpdateService epicUpdateService, IUserContext userContext)
    {
        _epicRepository = epicRepository;
        _epicCreationService = epicCreationService;
        _epicUpdateService = epicUpdateService;
        _userContext = userContext;
    }

    public async Task<T> GetEpicAsync<T>(Guid? id) where T : Epic => await _epicRepository.GetEpicAsync<T>(id);
    
    public async Task<IEnumerable<Epic>> GetAllEpicAsync() => await _epicRepository.GetAllEpicAsync();
    
    public async Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command)
    {
        var epic = new FinalEpic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        var userJobTitle = _userContext.GetCurrentUserJobTitle();
        await _epicCreationService.CreateFinalEpicForRestrictedJobTitles(_epicRepository, epic, userJobTitle);

        return epic.Id;
    }
    
    public async Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command)
    {
        var epic = new DraftEpic(command.Id, command.Name, command.CreatedDate);
        var userJobTitle = _userContext.GetCurrentUserJobTitle();
        await _epicCreationService.CreateDraftEpicForRestrictedJobTitles(_epicRepository, epic, userJobTitle);

        return epic.Id;
    }
    
    public async Task<bool> UpdateFinalEpicAsync(UpdateFinalEpic command)
    {
        var existingEpic = await _epicRepository.GetEpicAsync<FinalEpic>(command.Id);

        if (existingEpic is null)
        {
            return false;
        }

        var userJobTitle = _userContext.GetCurrentUserJobTitle();
        await _epicUpdateService.UpdateFinalEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, command.Status, command.Description, command.AcceptanceCriteria, userJobTitle);
        return true;
    }
    
    public async Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command)
    {
        var existingEpic = await _epicRepository.GetEpicAsync<DraftEpic>(command.Id);

        if (existingEpic is null)
        {
            return false;
        }

        var userJobTitle = _userContext.GetCurrentUserJobTitle();
        await _epicUpdateService.UpdateDraftEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, userJobTitle);
        return true;
    }
    
    public async Task<bool> DeleteEpicAsync(DeleteEpic command)
    {
        var existingEpic = await GetEpicAsync<Epic>(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        await _epicRepository.DeleteEpicAsync(existingEpic);
        return true;
    }
}