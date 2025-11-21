using AgileBoard.Application.Commands;
using AgileBoard.Application.Services.EpicsService;
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.DomainServices.Update;
using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using AgileBoard.Core.ValueObjects;
using AgileBoard.Infrastructure.DAL.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.Services;

public class EpicsServiceTests
{
    [Fact]
    public async Task given_epic_create_final_with_valid_parameters_should_pass()
    { 
       var epicId = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand with { Name = _createFinalEpicCommand.Name });
       
       epicId.ShouldNotBeNull();
       
       ValidateCreatedFinalEpic(await _epicsService.GetEpicAsync<FinalEpic>(epicId), _createFinalEpicCommand);
    }
    
    [Fact]
    public async Task given_epic_create_draft_with_valid_parameters_should_pass()
    { 
        var testId = Guid.NewGuid(); 
        var epicId = await _epicsService.CreateDraftEpicAsync(_createDraftEpicCommand with { Id = testId  });
       
        epicId.ShouldNotBeNull();
        epicId.Value.ShouldBe(testId);
       
        ValidateCreatedDraftEpic(await _epicsService.GetEpicAsync<DraftEpic>(epicId), _createDraftEpicCommand);
    }
    
    [Fact (Skip = "Need some investigation.")]
    public async Task given_epic_update_final_with_valid_parameters_should_pass()
    {
        var epicId = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand);
        var updatedEpic = await _epicsService.GetEpicAsync<FinalEpic>(epicId);

        var isUpdated = await _epicsService.UpdateFinalEpicAsync(_updateFinalEpicCommand);
        isUpdated.ShouldBeTrue();

        ValidateUpdatedFinalEpic(updatedEpic!, _updateFinalEpicCommand);
    }
    
    [Fact]
    public async Task given_epic_update_draft_with_valid_parameters_should_pass()
    {
        var epicId = await _epicsService.CreateDraftEpicAsync(_createDraftEpicCommand);
        var updatedEpic = await _epicsService.GetEpicAsync<DraftEpic>(epicId);

        var isUpdated = await _epicsService.UpdateDraftEpicAsync(_updateDraftEpicCommand);
        isUpdated.ShouldBeTrue();

        ValidateUpdatedDraftEpic(updatedEpic!, _updateFinalEpicCommand);
    }
    
    [Fact]
    public async Task given_epic_update_with_not_existing_epic_should_throw_exception()
    {
        // ASSERT
        Func<Task> testCode = () => _epicsService.UpdateFinalEpicAsync(_updateFinalEpicCommand with { Id = Guid.NewGuid() });
        
        // ACT
        var exception = await Record.ExceptionAsync(testCode);
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EpicDoesNotExistException>();
        exception.Message.ShouldBe("Epic does not exist.");
    }
    
    [Fact]
    public async Task given_epic_delete_with_existing_id_should_pass()
    {
        var epicId = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand with { Id = Guid.NewGuid()});

        var isDeleted = await _epicsService.DeleteEpicAsync(new DeleteEpic(epicId));
        isDeleted.ShouldBeTrue();
    }
    
    [Fact]
    public async Task given_epic_delete_with_not_existing_id_should_throw_exception()
    {
        // ASSERT
        Func<Task> testCode = () => _epicsService.DeleteEpicAsync(new DeleteEpic(Guid.NewGuid()));
        
        // ACT
        var exception = await Record.ExceptionAsync(testCode);
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EpicDoesNotExistException>();
        exception.Message.ShouldBe("Epic does not exist.");
    }
    
    [Fact]
    public async Task given_epic_get_with_existing_id_should_pass()
    {
        var epicId = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand with { Id = Guid.NewGuid()});

        var epic = await _epicsService.GetEpicAsync<FinalEpic>(epicId);
        epic.ShouldNotBeNull();
        ValidateCreatedFinalEpic(epic, _createFinalEpicCommand);
    }
    
    [Fact (Skip = "Work in progress. Failing on Collection was modified; enumeration operation may not execute.")]
    public async Task given_epic_get_all_with_existing_ids_should_pass()
    {
        await Cleanup();
        
        var epicId1 = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand with { Id = Guid.NewGuid()});
        var epicId2 = await _epicsService.CreateFinalEpicAsync(_createFinalEpicCommand with { Id = Guid.NewGuid()});
        
        var allEpics = await _epicsService.GetAllEpicAsync();
        allEpics.ShouldSatisfyAllConditions(
                () => allEpics.ShouldNotBeNull(),
                () => allEpics.Count().ShouldBe(2),
                () => allEpics.First().Id.ShouldBe<EpicId>(epicId1),
                () => allEpics.Last().Id.ShouldBe<EpicId>(epicId2));
    }

    #region Arrange

    private readonly IEpicsService _epicsService;
    private readonly CreateFinalEpic _createFinalEpicCommand;
    private readonly CreateDraftEpic _createDraftEpicCommand;
    private readonly UpdateFinalEpic _updateFinalEpicCommand;
    private readonly UpdateDraftEpic _updateDraftEpicCommand;
    private static readonly Guid EpicId = Guid.NewGuid();

    public EpicsServiceTests()
    {
        IEpicRepository epicRepository = new InMemoryEpicRepository();
        var businessAnalystEpicCreationPolicy = new Mock<BusinessAnalystEpicCreationPolicy>().Object;
        var policies = new List<IEpicPolicy>();
        policies.Add(businessAnalystEpicCreationPolicy);
        IEpicCreationService epicCreationService = new EpicCreationService(policies);
        IEpicUpdateService epicEpicUpdateService = new EpicUpdateService(policies);
        _epicsService = new EpicsService(epicRepository, epicCreationService, epicEpicUpdateService);
        _createFinalEpicCommand = new CreateFinalEpic(EpicId, "Name", "New", "Description", "AcceptanceCriteria", DateTimeOffset.Now);
        _createDraftEpicCommand = new CreateDraftEpic(EpicId, "Name", DateTimeOffset.Now);
        _updateFinalEpicCommand = new UpdateFinalEpic(EpicId, "NameUpdated", "NewUpdated", "DescriptionUpdated", "AcceptanceCriteriaUpdated");
        _updateDraftEpicCommand = new UpdateDraftEpic(EpicId, "NameUpdated");
    }

    private static void ValidateUpdatedFinalEpic(FinalEpic actualEpic, UpdateFinalEpic expectedEpic)
    {
        actualEpic.Name.ShouldBe<Name>(expectedEpic.Name);
        actualEpic.Status.ShouldBe<Status>(expectedEpic.Status);
        actualEpic.Description.ShouldBe<Description>(expectedEpic.Description);
        actualEpic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(expectedEpic.AcceptanceCriteria);
    }
    
    private static void ValidateUpdatedDraftEpic(DraftEpic actualEpic, UpdateFinalEpic expectedEpic)
    {
        actualEpic.Name.ShouldBe<Name>(expectedEpic.Name);
    }
    
    private static void ValidateCreatedFinalEpic(FinalEpic? finalActualEpic, CreateFinalEpic expectedEpic)
    {
        finalActualEpic!.Name.ShouldBe<Name>(expectedEpic.Name);
        finalActualEpic.Status.ShouldBe<Status>(expectedEpic.Status);
        finalActualEpic.Description.ShouldBe<Description>(expectedEpic.Description);
        finalActualEpic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(expectedEpic.AcceptanceCriteria);
        finalActualEpic.CreatedDate.ShouldBe<Date>(expectedEpic.CreatedDate);
    }
    
    private static void ValidateCreatedDraftEpic(DraftEpic? finalActualEpic, CreateDraftEpic expectedEpic)
    {
        finalActualEpic!.Name.ShouldBe<Name>(expectedEpic.Name);
        finalActualEpic.CreatedDate.ShouldBe<Date>(expectedEpic.CreatedDate);
    }

    private async Task Cleanup()
    {
        var allEpics = await _epicsService.GetAllEpicAsync();
        foreach (var epic in allEpics)
        {
            await _epicsService.DeleteEpicAsync(new DeleteEpic(epic.Id));
        }
    }

    #endregion
}