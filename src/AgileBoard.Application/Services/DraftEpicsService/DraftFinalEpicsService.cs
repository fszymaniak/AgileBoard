using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.DraftEpicsService;
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.DomainServices.Update;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Application.Services.DraftEpicsService;

public sealed class DraftFinalEpicsService : IDraftEpicsService
{
    private readonly IDraftEpicRepository _draftEpicRepository;
    private readonly IEpicCreationService _epicCreationService;
    private readonly IEpicUpdateService _epicUpdateService;
    
    public DraftFinalEpicsService(IDraftEpicRepository draftEpicRepository, IEpicCreationService epicCreationService, IEpicUpdateService epicUpdateService)
    {
        _draftEpicRepository = draftEpicRepository;
        _epicCreationService = epicCreationService;
        _epicUpdateService = epicUpdateService;
    }

    public async Task<DraftEpic> GetDraftEpicAsync(Guid? id) => await _draftEpicRepository.GetDraftEpicAsync(id);

    public async Task<IEnumerable<DraftEpic>> GetAllDraftEpicAsync() => await _draftEpicRepository.GetAllDraftEpicAsync();
    
    public async Task<Guid?> CreateDraftEpicAsync(CreateDraftEpic command)
    {
        var epic = new DraftEpic(command.Id, command.Name, command.CreatedDate);
        await _epicCreationService.CreateDraftEpicForRestrictedJobTitles(_draftEpicRepository, epic, JobTitle.ProductOwner);

        return epic.Id;
    }

    public async Task<bool> UpdateDraftEpicAsync(UpdateDraftEpic command)
    {
        var existingEpic = await _draftEpicRepository.GetDraftEpicAsync(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        await _epicUpdateService.UpdateDraftEpicForRestrictedJobTitles(_draftEpicRepository, (DraftEpic)existingEpic, command.Name, JobTitle.DevelopmentTeamMember);
        return true;
    }

    public async Task<bool> DeleteDraftEpicAsync(DeleteEpic command)
    {
        var existingEpic = await GetDraftEpicAsync(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        await _draftEpicRepository.DeleteDraftEpicAsync(existingEpic);
        return true;
    }
}