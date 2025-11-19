using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.Entities;

public class FinalEpicTests
{
    #region Assert

    private readonly FinalEpic _finalEpic;

    public FinalEpicTests()
    {
        _finalEpic = new FinalEpic(Guid.NewGuid(), "name", "status", "description", "acceptanceCriteria", DateTimeOffset.Now);
    }

    #endregion
    
    
    [Fact]
    public void given_empty_name_change_finalEpic_throw_exception()
    {
        // ARRANGE
        var emptyName = string.Empty;

        // ACT
        var exception = Record.Exception(() => _finalEpic.ChangeName(emptyName)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyNameException>();
        exception.Message.ShouldBe("Name is empty.");
    }
    
    [Fact]
    public void given_empty_status_change_finalEpic_throw_exception()
    {
        // ARRANGE
        var emptyStatus = string.Empty;

        // ACT
        var exception = Record.Exception(() => _finalEpic.ChangeStatus(emptyStatus)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyStatusException>();
        exception.Message.ShouldBe("Status is empty.");
    }
    
    [Fact]
    public void given_empty_description_change_finalEpic_throw_exception()
    {
        // ARRANGE
        var emptyDescription = string.Empty;

        // ACT
        var exception = Record.Exception(() => _finalEpic.ChangeDescription(emptyDescription)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyDescriptionException>();
        exception.Message.ShouldBe("Description is empty.");
    }
    
    [Fact]
    public void given_description_over_the_limit_change_finalEpic_throw_exception()
    {
        // ARRANGE
        var rand = new Random();
        var descriptionOverMaxLimit = string.Join("", Enumerable.Repeat(0, 501).Select(n => (char)rand.Next(127)));

        // ACT
        var exception = Record.Exception(() => _finalEpic.ChangeDescription(descriptionOverMaxLimit)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<DescriptionOverMaxCharLimitException>();
        exception.Message.ShouldBe("Description over the max characters limit.");
    }
    
    [Fact]
    public void given_empty_acceptance_criteria_change_finalEpic_throw_exception()
    {
        // ARRANGE
        var emptyAcceptanceCriteria = string.Empty;

        // ACT
        var exception = Record.Exception(() => _finalEpic.ChangeAcceptanceCriteria(emptyAcceptanceCriteria)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyAcceptanceCriteriaException>();
        exception.Message.ShouldBe("Acceptance Criteria is empty.");
    }

    [Fact]
    public void given_finalEpic_fields_change_should_succeed()
    {
        // ARRANGE
        var updatedName = "updatedName";
        var updatedStatus = "updatedStatus";
        var updatedDescription = "updatedDescription";
        var updatedAcceptanceCriteria = "updatedAcceptanceCriteria";
        
        // ACT
        _finalEpic.ChangeName(updatedName);
        _finalEpic.ChangeStatus(updatedStatus);
        _finalEpic.ChangeDescription(updatedDescription);
        _finalEpic.ChangeAcceptanceCriteria(updatedAcceptanceCriteria);
        
        // ASSERT
        _finalEpic.Name.ShouldBe<Name>(updatedName);
        _finalEpic.Status.ShouldBe<Status>(updatedStatus);
        _finalEpic.Description.ShouldBe<Description>(updatedDescription);
        _finalEpic.AcceptanceCriteria.ShouldBe<AcceptanceCriteria>(updatedAcceptanceCriteria);
    }
}