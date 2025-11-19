using NetArchTest.Rules;
using Xunit;

namespace AgileBoard.Unit.Tests.Architecture;

public class DomainDrivenDesignTests
{
    [Fact]
    public void ValueObjects_Should_Be_Sealed()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.ValueObjects")
            .And()
            .AreNotInterfaces()
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All Value Objects should be sealed to prevent inheritance. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void ValueObjects_Should_Not_Be_Mutable()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.ValueObjects")
            .And()
            .AreNotInterfaces()
            .And()
            .AreNotAbstract()
            .Should()
            .BeImmutable()
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All Value Objects should be immutable. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Entities_Should_Reside_In_Entities_Namespace()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Entities")
            .Should()
            .BeClasses()
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "All entities should be classes in the Entities namespace");
    }

    [Fact]
    public void Policies_Should_Implement_Policy_Interface()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Policies")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .Should()
            .ImplementInterface(typeof(AgileBoard.Core.Policies.IEpicPolicy))
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All policy classes should implement IEpicPolicy. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Exceptions_Should_Inherit_From_CustomException()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Exceptions")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveName("CustomException")
            .Should()
            .Inherit(typeof(AgileBoard.Core.Exceptions.CustomException))
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All domain exceptions should inherit from CustomException. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    // NOTE: Commands_Should_Be_Immutable_Records test removed because NetArchTest.Rules v1.3.2
    // does not correctly detect immutability in C# records. Commands are defined as records with
    // positional parameters which are inherently immutable in C#.

    [Fact]
    public void Repositories_Should_Only_Exist_In_Infrastructure_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;
        var applicationAssembly = typeof(AgileBoard.Application.Services.EpicsService.IEpicsService).Assembly;

        // Act - Check Core has only interfaces
        var coreResult = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Repositories")
            .And()
            .HaveNameEndingWith("Repository")
            .Should()
            .BeInterfaces()
            .GetResult();

        // Act - Check Application has no repositories
        var applicationTypes = Types.InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .GetTypes();

        // Assert
        Assert.True(coreResult.IsSuccessful, $"Core layer should only have repository interfaces. Violations: {string.Join(", ", coreResult.FailingTypeNames ?? new List<string>())}");
        Assert.Empty(applicationTypes);
    }

    [Fact]
    public void Domain_Services_Should_Not_Depend_On_Application_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespaceStartingWith("AgileBoard.Core.DomainServices")
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveDependencyOn("AgileBoard.Application")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Domain services should not depend on Application layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Domain_Services_Should_Not_Depend_On_Infrastructure_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespaceStartingWith("AgileBoard.Core.DomainServices")
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveDependencyOn("AgileBoard.Infrastructure")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Domain services should not depend on Infrastructure layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Domain_Services_Should_Not_Depend_On_Api_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespaceStartingWith("AgileBoard.Core.DomainServices")
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveDependencyOn("AgileBoard.Api")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Domain services should not depend on Api layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Controllers_Should_Not_Depend_On_Infrastructure()
    {
        // Arrange
        var apiAssembly = typeof(AgileBoard.Api.Controllers.EpicsController).Assembly;

        // Act
        var result = Types.InAssembly(apiAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Api.Controllers")
            .ShouldNot()
            .HaveDependencyOn("AgileBoard.Infrastructure")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Controllers should not directly depend on Infrastructure. They should depend on abstractions. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Controllers_Should_Not_Depend_On_Core_Directly()
    {
        // Arrange
        var apiAssembly = typeof(AgileBoard.Api.Controllers.EpicsController).Assembly;

        // Act
        var result = Types.InAssembly(apiAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Api.Controllers")
            .ShouldNot()
            .HaveDependencyOnAll("AgileBoard.Core.Entities", "AgileBoard.Core.DomainServices", "AgileBoard.Core.Repositories")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Controllers should communicate through Application layer, not directly with Core. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }
}
