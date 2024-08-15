using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class AcceptanceCriteriaTests
{
    [Theory]
    [InlineData("AcceptanceCriteriaTest", "AcceptanceCriteriaTest")]
    [InlineData("10", "10")]
    public void given_value_object_acceptance_criteria_is_properly_created_by_new_instance(string acceptanceCriteria, string expectedAcceptanceCriteria)
    {
        // Act
        var result = new AcceptanceCriteria(acceptanceCriteria);

        // Assert
        result.Value.ShouldBe(expectedAcceptanceCriteria);
    }
    
    [Fact]
    public void given_value_object_acceptance_criteria_is_properly_created_by_implicit_operator_string()
    {
        // Act
        const string acceptanceCriteriaString = "AcceptanceCriteria01";
        var result = new AcceptanceCriteria(acceptanceCriteriaString);
        string resultAcceptanceCriteria = result;

        // Assert
        resultAcceptanceCriteria.ShouldBe(acceptanceCriteriaString);
    }
    
    [Fact]
    public void given_value_object_acceptance_criteria_is_properly_created_by_implicit_operator_acceptanceCriteria()
    {
        // Act
        const string acceptanceCriteriaString = "AcceptanceCriteria01";
        AcceptanceCriteria result = acceptanceCriteriaString;

        // Assert
        result.Value.ShouldBe(acceptanceCriteriaString);
    }
    
    [Fact]
    public void given_value_object_acceptance_criteria_is_null_then_exception_is_thrown()
    {
        // Act
        Action act = () =>
        {
            var acceptanceCriteria = new AcceptanceCriteria(null);
        };

        // Assert
        act.ShouldThrow<EmptyAcceptanceCriteriaException>();
    }
    
    
}