using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.EpicsService;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using AgileBoard.Infrastructure.DAL.Repositories;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.Services;

public class EpicsServiceTests
{
    [Fact]
    public async Task given_epic_create_with_valid_parameters_should_pass()
    { 
       var testId = Guid.NewGuid(); 
       var epicId = await _epicsService.CreateAsync(_createCommand with { Id = testId  });
       
       epicId.ShouldNotBeNull();
       epicId.Value.ShouldBe(testId);
       
       ValidateCreatedEpic(await _epicsService.GetAsync(epicId), _createCommand);
    }
    
    [Fact]
    public async Task given_epic_update_with_valid_parameters_should_pass()
    {
        var epicId = await _epicsService.CreateAsync(_createCommand);
        var updatedEpic = await _epicsService.GetAsync(epicId);

        var isUpdated = await _epicsService.UpdateAsync(_updateCommand);
        isUpdated.ShouldBeTrue();

        ValidateUpdatedEpic(updatedEpic, _updateCommand);
    }
    
    [Fact]
    public async Task given_epic_update_with_not_existing_epic_should_throw_exception()
    {
        // ASSERT
        Func<Task> testCode = () => _epicsService.UpdateAsync(_updateCommand with { Id = Guid.NewGuid() });
        
        // ACT
        var exception = await Record.ExceptionAsync(testCode);
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EpicDoesNotExist>();
        exception.Message.ShouldBe("Epic does not exist.");
    }
    
    [Fact]
    public async Task given_epic_delete_with_existing_id_should_pass()
    {
        var epicId = await _epicsService.CreateAsync(_createCommand with { Id = Guid.NewGuid()});

        var isDeleted = await _epicsService.DeleteAsync(new DeleteEpic(epicId));
        isDeleted.ShouldBeTrue();
    }
    
    [Fact]
    public async Task given_epic_delete_with_not_existing_id_should_throw_exception()
    {
        // ASSERT
        Func<Task> testCode = () => _epicsService.DeleteAsync(new DeleteEpic(Guid.NewGuid()));
        
        // ACT
        var exception = await Record.ExceptionAsync(testCode);
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EpicDoesNotExist>();
        exception.Message.ShouldBe("Epic does not exist.");
    }
    
    [Fact]
    public async Task given_epic_get_with_existing_id_should_pass()
    {
        var epicId = await _epicsService.CreateAsync(_createCommand with { Id = Guid.NewGuid()});

        var epic = await _epicsService.GetAsync(epicId);
        epic.ShouldNotBeNull();
        ValidateCreatedEpic(epic, _createCommand);
    }
    
    [Fact (Skip = "Work in progress. Failing on Collection was modified; enumeration operation may not execute.")]
    public async Task given_epic_get_all_with_existing_ids_should_pass()
    {
        await Cleanup();
        
        var epicId1 = await _epicsService.CreateAsync(_createCommand with { Id = Guid.NewGuid()});
        var epicId2 = await _epicsService.CreateAsync(_createCommand with { Id = Guid.NewGuid()});
        
        var allEpics = await _epicsService.GetAllAsync();
        allEpics.ShouldSatisfyAllConditions(
                () => allEpics.ShouldNotBeNull(),
                () => allEpics.Count().ShouldBe(2),
                () => allEpics.First().Id.ShouldBe<EpicId>(epicId1),
                () => allEpics.Last().Id.ShouldBe<EpicId>(epicId2));
    }

    #region Arrange

    private readonly IEpicsService _epicsService;
    private readonly CreateEpic _createCommand;
    private readonly UpdateEpic _updateCommand;
    private static readonly Guid EpicId = Guid.NewGuid();

    public EpicsServiceTests()
    {
        IEpicRepository epicRepository = new InMemoryEpicRepository();
        _epicsService = new EpicsService(epicRepository);
        _createCommand = new CreateEpic(EpicId, "Name", "New", "Description", "AcceptanceCriteria", DateTimeOffset.Now);
        _updateCommand = new UpdateEpic(EpicId, "NameUpdated", "NewUpdated", "DescriptionUpdated", "AcceptanceCriteriaUpdated");
    }

    private static void ValidateUpdatedEpic(Epic actualEpic, UpdateEpic expectedEpic)
    {
        actualEpic.Name.ShouldBe<Name>(expectedEpic.Name);
        actualEpic.Status.ShouldBe<Status>(expectedEpic.Status);
        actualEpic.Description.ShouldBe<Description>(expectedEpic.Description);
        actualEpic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(expectedEpic.AcceptanceCriteria);
    }
    
    private static void ValidateCreatedEpic(Epic actualEpic, CreateEpic expectedEpic)
    {
        actualEpic.Name.ShouldBe<Name>(expectedEpic.Name);
        actualEpic.Status.ShouldBe<Status>(expectedEpic.Status);
        actualEpic.Description.ShouldBe<Description>(expectedEpic.Description);
        actualEpic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(expectedEpic.AcceptanceCriteria);
        actualEpic.CreatedDate.ShouldBe<Date>(expectedEpic.CreatedDate);
    }

    private async Task Cleanup()
    {
        var allEpics = await _epicsService.GetAllAsync();
        foreach (var epic in allEpics)
        {
            await _epicsService.DeleteAsync(new DeleteEpic(epic.Id));
        }
    }

    #endregion
}