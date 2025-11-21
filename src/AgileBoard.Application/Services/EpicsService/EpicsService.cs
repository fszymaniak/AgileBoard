using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.CurrentUserService;
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
    private readonly ICurrentUserService _currentUserService;

    public EpicsService(
        IEpicRepository epicRepository,
        IEpicCreationService epicCreationService,
        IEpicUpdateService epicUpdateService,
        ICurrentUserService currentUserService)
    {
        _epicRepository = epicRepository;
        _epicCreationService = epicCreationService;
        _epicUpdateService = epicUpdateService;
        _currentUserService = currentUserService;
    }

    public async Task<T> GetEpicAsync<T>(Guid? id) where T : Epic => await _epicRepository.GetEpicAsync<T>(id);
    
    public async Task<IEnumerable<Epic>> GetAllEpicAsync() => await _epicRepository.GetAllEpicAsync();
    
    public async Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command)
    {
        var currentUserJobTitle = _currentUserService.GetCurrentUserJobTitle();
        var epic = new FinalEpic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        await _epicCreationService.CreateFinalEpicForRestrictedJobTitles(_epicRepository, epic, currentUserJobTitle);

        return epic.Id;
    }

    public async Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command)
    {
        var currentUserJobTitle = _currentUserService.GetCurrentUserJobTitle();
        var epic = new DraftEpic(command.Id, command.Name, command.CreatedDate);
        await _epicCreationService.CreateDraftEpicForRestrictedJobTitles(_epicRepository, epic, currentUserJobTitle);

        return epic.Id;
    }
    
    public async Task<bool> UpdateFinalEpicAsync(UpdateFinalEpic command)
    {
        var currentUserJobTitle = _currentUserService.GetCurrentUserJobTitle();
        var existingEpic = await _epicRepository.GetEpicAsync<FinalEpic>(command.Id);

        if (existingEpic is null)
        {
            return false;
        }

        await _epicUpdateService.UpdateFinalEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, command.Status, command.Description, command.AcceptanceCriteria, currentUserJobTitle);
        return true;
    }

    public async Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command)
    {
        var currentUserJobTitle = _currentUserService.GetCurrentUserJobTitle();
        var existingEpic = await _epicRepository.GetEpicAsync<DraftEpic>(command.Id);

        if (existingEpic is null)
        {
            return false;
        }

        await _epicUpdateService.UpdateDraftEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, currentUserJobTitle);
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