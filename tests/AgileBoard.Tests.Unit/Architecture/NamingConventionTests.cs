using NetArchTest.Rules;
using Xunit;

namespace AgileBoard.Unit.Tests.Architecture;

public class NamingConventionTests
{
    [Fact]
    public void Controllers_Should_Have_Controller_Suffix()
    {
        // Arrange
        var apiAssembly = typeof(AgileBoard.Api.Controllers.EpicsController).Assembly;

        // Act
        var result = Types.InAssembly(apiAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Api.Controllers")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All controllers should end with 'Controller'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Repositories_Should_Have_Repository_Suffix()
    {
        // Arrange
        var infrastructureAssembly = typeof(AgileBoard.Infrastructure.DAL.AgileBoardDbContext).Assembly;

        // Act
        var result = Types.InAssembly(infrastructureAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Infrastructure.DAL.Repositories")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All repository implementations should end with 'Repository'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Repository_Interfaces_Should_Start_With_I()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Repositories")
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All repository interfaces should start with 'I'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Services_Should_Have_Service_Suffix()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Application.Services")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("Service")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All service implementations should end with 'Service'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Domain_Services_Should_Have_Service_Suffix()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.DomainServices")
            .And()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("Service")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All domain service implementations should end with 'Service'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Service_Interfaces_Should_Start_With_I()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Application.Services")
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All service interfaces should start with 'I'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Policies_Should_Have_Policy_Suffix()
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
            .HaveNameEndingWith("Policy")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All policy implementations should end with 'Policy'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Policy_Interfaces_Should_Start_With_I()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Policies")
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All policy interfaces should start with 'I'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Exceptions_Should_Have_Exception_Suffix()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Core.Exceptions")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All custom exceptions should end with 'Exception'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void DTOs_Should_Have_Dto_Suffix()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace("AgileBoard.Application.DTO")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Dto")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"All DTOs should end with 'Dto'. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }
}
