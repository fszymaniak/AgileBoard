using AgileBoard.Core.Exceptions;
using AgileBoard.Core.ValueObjects;
using Moq;
using Shouldly;
using Xunit;

namespace AgileBoard.Unit.Tests.ValueObjects;

public class EpicIdTests
{
    [Fact]
    public void given_value_object_epicId_is_properly_created_by_new_instance()
    {
        // Act
        var epicIdGuid = Guid.NewGuid();
        var result = new EpicId(epicIdGuid);

        // Assert
        result.Value.ShouldBe(epicIdGuid);
    }
    
    [Fact]
    public void given_value_object_epicId_is_properly_created_by_implicit_operator()
    {
        // Act
        var epicIdGuid = Guid.NewGuid();
        EpicId result = epicIdGuid;

        // Assert
        result.Value.ShouldBe(epicIdGuid);
    }
    
    [Fact]
    public void given_value_object_epicId_is_properly_created_by_create_method()
    {
        // Act
        EpicId result = EpicId.Create();

        // Assert
        result.Value.ShouldBeOfType<Guid>();
        result.Value.ShouldNotBe(Guid.Empty);
    }
    
    [Fact]
    public void given_value_object_epicId_throws_exception_when_guid_is_empty()
    {
        // Act
        Action act = () =>
        {
            var epicId = new EpicId(Guid.Empty);
        };

        // Assert
        act.ShouldThrow<InvalidEntityIdException>();
    }
}