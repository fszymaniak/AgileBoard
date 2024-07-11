using AgileBoard.Application.Commands;
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
    
    public EpicsService(IEpicRepository epicRepository, IEpicCreationService epicCreationService, IEpicUpdateService epicUpdateService)
    {
        _epicRepository = epicRepository;
        _epicCreationService = epicCreationService;
        _epicUpdateService = epicUpdateService;
    }

    public async Task<T> GetEpicAsync<T>(Guid? id) where T : Epic => await _epicRepository.GetEpicAsync<T>(id);
    
    public async Task<IEnumerable<Epic>> GetAllEpicAsync() => await _epicRepository.GetAllEpicAsync();
    
    public async Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command)
    {
        var epic = new FinalEpic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        await _epicCreationService.CreateFinalEpicForRestrictedJobTitles(_epicRepository, epic, JobTitle.ProductOwner);

        return epic.Id;
    }
    
    public async Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command)
    {
        var epic = new DraftEpic(command.Id, command.Name, command.CreatedDate);
        await _epicCreationService.CreateDraftEpicForRestrictedJobTitles(_epicRepository, epic, JobTitle.ProductOwner);

        return epic.Id;
    }
    
    public async Task<bool> UpdateDraftEpicAsync(UpdateFinalEpic command)
    {
        var existingEpic = await _epicRepository.GetEpicAsync<FinalEpic>(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        await _epicUpdateService.UpdateFinalEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, command.Status, command.Description, command.AcceptanceCriteria, JobTitle.BusinessAnalyst);
        return true;
    }
    
    public async Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command)
    {
        var existingEpic = await _epicRepository.GetEpicAsync<DraftEpic>(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        await _epicUpdateService.UpdateDraftEpicForRestrictedJobTitles(_epicRepository, existingEpic, command.Name, JobTitle.BusinessAnalyst);
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