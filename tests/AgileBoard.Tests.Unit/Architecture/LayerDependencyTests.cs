using NetArchTest.Rules;
using Xunit;

namespace AgileBoard.Unit.Tests.Architecture;

public class LayerDependencyTests
{
    private const string CoreNamespace = "AgileBoard.Core";
    private const string ApplicationNamespace = "AgileBoard.Application";
    private const string InfrastructureNamespace = "AgileBoard.Infrastructure";
    private const string ApiNamespace = "AgileBoard.Api";

    [Fact]
    public void Core_Should_Not_Depend_On_Application_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace(CoreNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApplicationNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Core layer should not depend on Application layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Core_Should_Not_Depend_On_Infrastructure_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace(CoreNamespace)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Core layer should not depend on Infrastructure layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Core_Should_Not_Depend_On_Api_Layer()
    {
        // Arrange
        var coreAssembly = typeof(AgileBoard.Core.Entities.Epic).Assembly;

        // Act
        var result = Types.InAssembly(coreAssembly)
            .That()
            .ResideInNamespace(CoreNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Core layer should not depend on Api layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Application_Should_Not_Depend_On_Infrastructure_Layer()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.EpicsService.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Application layer should not depend on Infrastructure layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Application_Should_Not_Depend_On_Api_Layer()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.EpicsService.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Application layer should not depend on Api layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }

    [Fact]
    public void Application_Should_Depend_On_Core_Layer()
    {
        // Arrange
        var applicationAssembly = typeof(AgileBoard.Application.Services.EpicsService.IEpicsService).Assembly;

        // Act
        var result = Types.InAssembly(applicationAssembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .Should()
            .HaveDependencyOn(CoreNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Application layer should depend on Core layer. This is expected in Clean Architecture.");
    }

    [Fact]
    public void Infrastructure_Should_Depend_On_Core_Layer()
    {
        // Arrange
        var infrastructureAssembly = typeof(AgileBoard.Infrastructure.DAL.AgileBoardDbContext).Assembly;

        // Act
        var result = Types.InAssembly(infrastructureAssembly)
            .That()
            .ResideInNamespace(InfrastructureNamespace)
            .And()
            .ResideInNamespace("AgileBoard.Infrastructure.DAL")
            .Should()
            .HaveDependencyOn(CoreNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Infrastructure layer should depend on Core layer for entity definitions.");
    }

    [Fact]
    public void Infrastructure_Should_Not_Depend_On_Api_Layer()
    {
        // Arrange
        var infrastructureAssembly = typeof(AgileBoard.Infrastructure.DAL.AgileBoardDbContext).Assembly;

        // Act
        var result = Types.InAssembly(infrastructureAssembly)
            .That()
            .ResideInNamespace(InfrastructureNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Infrastructure layer should not depend on Api layer. Violations: {string.Join(", ", result.FailingTypeNames ?? new List<string>())}");
    }
}
