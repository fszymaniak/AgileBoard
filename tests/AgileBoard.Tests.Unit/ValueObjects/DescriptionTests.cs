using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class DescriptionTests
{
    [Theory]
    [InlineData("TestDescription", "TestDescription")]
    [InlineData("a", "a")]
    public void given_value_object_description_is_properly_created_by_new_instance(string description, string expectedName)
    {
        // Act
        var result = new Description(description);

        // Assert
        result.Value.ShouldBe(expectedName);
    }
    
    [Theory]
    [InlineData("TestDescription1", "TestDescription1")]
    [InlineData("description_description", "description_description")]
    public void given_value_object_description_is_properly_created_by_implicit_operator(string description, string expectedName)
    {
        // Act
        Description result = description;
        string descriptionString = result;

        // Assert
        descriptionString.ShouldBe(expectedName);
    }
    
    [Fact]
    public void given_null_in_value_object_description_throws_exception()
    {
        // Act
        Action act = () => new Description(null);

        // Assert
        act.ShouldThrow<EmptyDescriptionException>();
    }
    
    [Fact]
    public void given_over_500_chars_in_value_object_description_throws_exception()
    {
        // Act
        string descriptionOverMaxCharLimit =
            @"test12312391248210348120912849012841902482048048120sdfsdfsdfsd9481rt240912fkhsfhsakjdaskhfadsgsdkaskldajsdaslkdajsldajsdladasda
            sdanvcxasdasdasdmnskhaldjaskldjalksdjasldasjdaskljdalskdjaslkdjaslsdjslkdjaslsdsaksddbfvkjajskdasjdldjasljsdawdjas328904radsldjad
            lkasjdlasdassasd84091241241284012481290481240124809481209481240981240128401284120948120948120924812048124081209812402412098124019
            248029482024120jdkahdkjasdaskdahdjsdhaskbvsdjfhaskjsdaggfsfsdgdfhfggsdfsdsgsdfsdfsdfsdfsdfsdfgfjgfhdfgdssfsdfsddfsdf";
        
        Action act = () =>
        {
            var description = new Description(descriptionOverMaxCharLimit);
        };

        // Assert
        act.ShouldThrow<DescriptionOverMaxCharLimit>();
    }
}