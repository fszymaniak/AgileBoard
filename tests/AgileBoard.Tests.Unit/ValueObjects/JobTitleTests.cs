using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class JobTitleTests
{
    [Fact]
    public void given_value_object_job_title_is_properly_created_by_implicit_operator_JobTitle()
    {
        // Act
        const string jobTitleString = "Programmer";
        JobTitle jobTitle = jobTitleString;

        // Assert
        jobTitle.Value.ShouldBe(jobTitleString);
    }
}