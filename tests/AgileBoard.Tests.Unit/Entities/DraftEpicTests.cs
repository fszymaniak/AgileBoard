using AgileBoard.Core.Entities;
using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.Entities;

public class DraftEpicTests
{
    #region Assert

    private readonly DraftEpic _draftEpic;

    public DraftEpicTests()
    {
        _draftEpic = new DraftEpic(Guid.NewGuid(), "name", DateTimeOffset.Now);
    }

    #endregion
    
    
    [Fact]
    public void given_empty_name_change_draftEpic_throw_exception()
    {
        // ARRANGE
        var emptyName = string.Empty;

        // ACT
        var exception = Record.Exception(() => _draftEpic.ChangeName(emptyName)); 
            
        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyNameException>();
        exception.Message.ShouldBe("Name is empty.");
    }
    
    [Fact]
    public void given_draftEpic_fields_change_should_succeed()
    {
        // ARRANGE
        var updatedName = "updatedName";
        
        // ACT
        _draftEpic.ChangeName(updatedName);
        
        // ASSERT
        _draftEpic.Name.ShouldBe<Name>(updatedName);
    }
}