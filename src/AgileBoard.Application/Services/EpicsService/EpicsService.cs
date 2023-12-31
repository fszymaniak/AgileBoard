﻿using AgileBoard.Application.Commands;
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

    public async Task<Epic> GetAsync(Guid? id) => await _epicRepository.GetAsync(id);

    public async Task<IEnumerable<Epic>> GetAllAsync() => await _epicRepository.GetAllAsync();
    
    public async Task<Guid?> CreateAsync(CreateEpic command)
    {
        var epic = new Epic(command.Id, command.Name, command.Status, command.Description, command.AcceptanceCriteria, command.CreatedDate);
        await _epicRepository.AddAsync(epic);

        return epic.Id;
    }

    public async Task<bool> UpdateAsync(UpdateEpic command)
    {
        var existingEpic = await _epicRepository.GetAsync(command.Id);
        
        if (existingEpic is null)
        {
            return false;
        }
        
        existingEpic.ChangeName(command.Name);
        existingEpic.ChangeStatus(command.Status);
        existingEpic.ChangeDescription(command.Description);
        existingEpic.ChangeAcceptanceCriteria(command.AcceptanceCriteria);
        
        await _epicRepository.UpdateAsync(existingEpic);
        return true;
    }

    public async Task<bool> DeleteAsync(DeleteEpic command)
    {
        var existingEpic = await GetAsync(command.EpicId);
        
        if (existingEpic is null)
        {
            return false;
        }

        await _epicRepository.DeleteAsync(existingEpic);
        return true;
    }
}