using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class DateTests
{
    [Fact]
    public void given_value_object_date_is_properly_created_by_new_instance()
    {
        // Act
        string dateString = "05/01/2024";
        var dateTimeOffset = DateTimeOffset.Parse(dateString);
        var result = new Date(dateTimeOffset);

        // Assert
        result.Value.ShouldBe(dateTimeOffset);
    }
    
    [Fact]
    public void given_value_object_date_is_properly_created_by_implicit_operator_date()
    {
        // Act
        string dateString = "05/01/2024";
        var dateTimeOffset = DateTimeOffset.Parse(dateString);
        Date result = dateTimeOffset;

        // Assert
        result.Value.ShouldBe(dateTimeOffset);
    }
    
    [Fact]
    public void given_value_object_date_is_properly_created_by_implicit_operator_dateTimeOffset()
    {
        // Act
        string dateString = "05/01/2024";
        var dateTimeOffset = DateTimeOffset.Parse(dateString);
        var date = new Date(dateTimeOffset);
        DateTimeOffset result = date;

        // Assert
        result.ShouldBe(dateTimeOffset);
    }
    
    [Fact]
    public void given_value_object_date_now_method_is_working_properly()
    {
        // Act
        var dateNow = Date.Now;
        var expectedDateNow = (Date)DateTimeOffset.Now;

        // Assert
        dateNow.ShouldBeOfType<Date>();
        dateNow.ShouldNotBe(null);
        dateNow.ShouldBeEquivalentTo(expectedDateNow);
    }
}