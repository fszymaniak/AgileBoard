using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class StatusTests
{
    [Fact]
    public void given_value_object_status_is_properly_created_by_new_instance()
    {
        // Act
        const string statusString = "Created";
        Status status = new Status(statusString);

        // Assert
        status.Value.ShouldBe(statusString);
    }
    
    [Fact]
    public void given_value_object_status_is_properly_created_by_implicit_operator_Status()
    {
        // Act
        const string statusString = "Resolved";
        Status status = statusString;

        // Assert
        status.Value.ShouldBe(statusString);
    }
    
    [Fact]
    public void given_value_object_status_is_properly_created_by_implicit_operator_string()
    {
        // Act
        const string statusString = "Resolved";
        Status status = new Status(statusString);
        string result = status;

        // Assert
        result.ShouldBe(statusString);
    }
    
    [Fact]
    public void given_value_object_status_is_null_then_exception_is_thrown()
    {
        // Act
        Action act = () =>
        {
            var status = new Status(null);
        };
        
        // Assert
        act.ShouldThrow<EmptyStatusException>();
    }
}