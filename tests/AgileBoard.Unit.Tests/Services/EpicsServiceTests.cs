using AgileBoard.Api.Commands;
using AgileBoard.Api.Entities;
using AgileBoard.Api.Services.EpicsService;
using AgileBoard.Api.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.Services;

public class EpicsServiceTests
{
    [Fact]
    public void given_epic_create_with_valid_parameters_should_pass()
    {
       var epicId = _epicsService.Create(_createCommand);
       
       epicId.ShouldNotBeNull();
       epicId.Value.ShouldBe(_createCommand.Id);
       
       ValidateCreatedEpic(_epicsService.Get(epicId), _createCommand);
    }
    
    [Fact]
    public void given_epic_update_with_valid_parameters_should_pass()
    {
        var epicId = _epicsService.Create(_createCommand);
        var updatedEpic = _epicsService.Get(epicId);

        var isUpdated = _epicsService.Update(_updateCommand);
        isUpdated.ShouldBeTrue();

        ValidateUpdatedEpic(updatedEpic, _updateCommand);
    }
    
    [Fact]
    public void given_epic_update_with_not_existing_epic_should_fail()
    {
        var isUpdated = _epicsService.Update(_updateCommand with { Id = Guid.NewGuid()});
       
        isUpdated.ShouldBeFalse();
    }
    
    [Fact]
    public void given_epic_delete_with_existing_id_should_pass()
    {
        var epicId = _epicsService.Create(_createCommand with { Id = Guid.NewGuid()});

        var isDeleted = _epicsService.Delete(new DeleteEpic(epicId));
        isDeleted.ShouldBeTrue();
    }
    
    [Fact]
    public void given_epic_delete_with_not_existing_id_should_fail()
    {
        var isDeleted = _epicsService.Delete(new DeleteEpic(Guid.NewGuid()));
        isDeleted.ShouldBeFalse();
    }
    
    [Fact]
    public void given_epic_get_with_existing_id_should_pass()
    {
        var epicId = _epicsService.Create(_createCommand with { Id = Guid.NewGuid()});

        var epic = _epicsService.Get(epicId);
        epic.ShouldNotBeNull();
        ValidateCreatedEpic(epic, _createCommand);
    }
    
    [Fact]
    public void given_epic_get_all_with_existing_ids_should_pass()
    {
        Cleanup();
        
        var epicId1 = _epicsService.Create(_createCommand with { Id = Guid.NewGuid()});
        var epicId2 = _epicsService.Create(_createCommand with { Id = Guid.NewGuid()});
        
        var allEpics = _epicsService.GetAll();
        allEpics.ShouldNotBeNull();
        allEpics.Count().ShouldBe(2);
        allEpics.First().Id.ShouldBe<EpicId>(epicId1);
        allEpics.Last().Id.ShouldBe<EpicId>(epicId2);
    }

    #region Arrange

    private readonly IEpicsService _epicsService;
    private readonly CreateEpic _createCommand;
    private readonly UpdateEpic _updateCommand;
    private static readonly Guid EpicId = Guid.NewGuid();

    public EpicsServiceTests()
    {
        _epicsService = new EpicsService();
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

    private void Cleanup()
    {
        var allEpics = _epicsService.GetAll();
        foreach (var epic in allEpics)
        {
            _epicsService.Delete(new DeleteEpic(epic.Id));
        }
    }

    #endregion
}