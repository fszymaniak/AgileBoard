using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.FinalEpicsService;
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.DomainServices.Update;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;

namespace AgileBoard.Application.Services.FinalEpicsService;

public sealed class FinalFinalEpicsService : IFinalEpicsService
{
    private readonly IFinalEpicRepository _finalEpicRepository;
    private readonly IEpicCreationService _epicCreationService;
    private readonly IEpicUpdateService _epicUpdateService;
    
    public FinalFinalEpicsService(IFinalEpicRepository finalEpicRepository, IEpicCreationService epicCreationService, IEpicUpdateService epicUpdateService)
    {
        _finalEpicRepository = finalEpicRepository;
        _epicCreationService = epicCreationService;
        _epicUpdateService = epicUpdateService;
    }

    public async Task<FinalEpic> GetFinalEpicAsync(Guid? id) => await _finalEpicRepository.GetFinalEpicAsync(id);

    public async Task<IEnumerable<FinalEpic>> GetAllFinalEpicAsync() => await _finalEpicRepository.GetAllFinalEpicAsync();
    
    public async Task<Guid?> CreateFinalEpicAsync(CreateFinalEpic command)
    {
        var epic = new FinalEpic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        await _epicCreationService.CreateFinalEpicForRestrictedJobTitles(_finalEpicRepository, epic, JobTitle.ProductOwner);

        return epic.Id;
    }
    
    public async Task<bool> UpdateFinalEpicAsync(UpdateFinalEpic command)
    {
        var existingEpic = await _finalEpicRepository.GetFinalEpicAsync(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        await _epicUpdateService.UpdateFinalEpicForRestrictedJobTitles(_finalEpicRepository, (FinalEpic)existingEpic, command.Name, command.Status, command.Description, command.AcceptanceCriteria, JobTitle.BusinessAnalyst);
        return true;
    }
    
    public async Task<bool> DeleteFinalEpicAsync(DeleteEpic command)
    {
        var existingEpic = await GetFinalEpicAsync(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        await _finalEpicRepository.DeleteFinalEpicAsync(existingEpic);
        return true;
    }
}