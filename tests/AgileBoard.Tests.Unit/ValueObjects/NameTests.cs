using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Moq;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class NameTests
{
    [Theory]
    [InlineData("TestName", "TestName")]
    [InlineData("a", "a")]
    public void given_value_object_name_is_properly_created_by_new_instance(string name, string expectedName)
    {
        // Act
        var result = new Name(name);

        // Assert
        result.Value.ShouldBe(expectedName);
    }
    
    [Theory]
    [InlineData("TestName1", "TestName1")]
    [InlineData("name_name", "name_name")]
    public void given_value_object_name_is_properly_created_by_implicit_operator(string name, string expectedName)
    {
        // Act
        var result = new Name(name);
        string nameString = result;

        // Assert
        nameString.ShouldBe(expectedName);
    }
    
    [Fact]
    public void given_null_in_value_object_name_throws_exception()
    {
        // Act
        Action act = () => new Name(null);

        // Assert
        act.ShouldThrow<EmptyNameException>();
    }
    
    [Fact]
    public void given_value_object_name_is_set_properly_()
    {
        // Act
        string testName = "testName"; 
        string updatedTestName = "updatedTestName"; 
        Name name = new Name(testName);

        // Assert
        name.Value = updatedTestName;
        name.Value.ShouldBe(updatedTestName);
    }
    
}