using System.Runtime.CompilerServices;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Core.Repositories;
using AgileBoard.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("AgileBoard.Tests.Unit")]
namespace AgileBoard.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddSingleton<IClock, Clock>()
            .AddSingleton<IEpicRepository, InMemoryEpicRepository>();

        return services;
    }
}